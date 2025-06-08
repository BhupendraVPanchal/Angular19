import { EventEmitter, Injectable } from "@angular/core";
import { MatDialogConfig } from "@angular/material/dialog";

@Injectable()
export class M2GridHelper {

  public static break_event(event: any) {
    event.preventDefault();
    event.stopPropagation();
  }

  public static focus_cell(rows: any, row_idx: any, col_idx: any) {
    let all_rows = (rows.nativeElement as HTMLElement)?.querySelectorAll('.tr')
    let row;
    if (all_rows) row = all_rows[row_idx];
    if (row) (row.querySelector('[col-index="' + (col_idx) + '"]') as HTMLElement)?.focus();
  }

  public static next_sibling(viewport: HTMLElement, rows: HTMLElement, add_row: () => void, config: any, column_config: any[], active_row_data: any, element: HTMLElement, is_row: boolean) {
    event.preventDefault();
    let ele = event.target as HTMLElement;
    if (!is_row) {
      let next_ele = element.nextElementSibling as HTMLElement;
      if (next_ele && !next_ele.hasAttribute('hidden')) {
        next_ele.focus();
        next_ele.scrollIntoView({ inline: "end", block: "nearest" });
      } else if (next_ele && next_ele.hasAttribute('hidden')) {
        this.next_sibling(viewport, rows, add_row, config, column_config, active_row_data, next_ele, is_row);
      }
    }
    else {

      if (this.validate_row(config, column_config, active_row_data)) {
        let next_row = (ele.closest('.tr') as HTMLElement).nextElementSibling;
        let col_idx = ele.getAttribute('col-index');
        if (next_row) {
          let next_ele = next_row.querySelector('[col-index="' + col_idx + '"]') as HTMLElement;
          next_ele.focus();

          //next_ele.scrollIntoView({ inline: 'nearest', block: 'end' })
          next_ele.scrollIntoView({ inline: 'nearest', block: 'nearest' })

        } else {
          if (config.editable_grid && config.allow_add) {
            add_row();
            setTimeout((x:any) => {
              let next_row = (ele.closest('.tr') as HTMLElement).nextElementSibling;
              let col_idx = ele.getAttribute('col-index');
              if (next_row) {
                let next_ele = next_row.querySelector('[col-index="' + col_idx + '"]') as HTMLElement;
                next_ele.focus();
                next_ele.scrollIntoView({ inline: 'nearest', block: 'start' })
              }
            })
          }
        }

      } else {
        setTimeout((x:any) => {
          let row = rows.querySelector('.invalid-row') as HTMLElement;
          this.focus_cell(rows, row.getAttribute('row-idx'), 1);
        })
      }

    }
  }


  public static prev_sibling(config: any, column_config: any[], active_row_data: any, element: HTMLElement, is_row: boolean) {
    event.preventDefault();

    if (!is_row) {
      let prev_ele = element.previousElementSibling as HTMLElement;
      if (prev_ele && !prev_ele.hasAttribute('hidden')) {
        prev_ele.focus();
        prev_ele.scrollIntoView({ inline: "end", block: "nearest" });
      } else if (prev_ele && prev_ele.hasAttribute('hidden')) {
        this.prev_sibling(config, column_config, active_row_data, prev_ele, is_row);
      }
    }
    else {

      if (active_row_data['row_state'] == 'n' && !column_config.find(x => x['IsEnable'] && x['IsVisible'] && active_row_data[x['ColumnName']])) this.prev_row(event);
      else if (this.validate_row(config, column_config, active_row_data)) this.prev_row(event);
    }
  }

  public static prev_row(event: any) {
    let prev_row = ((event.target as HTMLElement).closest('.tr') as HTMLElement).previousElementSibling;
    let col_idx = (event.target as HTMLElement).getAttribute('col-index');
    if (prev_row) {
      let prev_ele = prev_row.querySelector('[col-index="' + col_idx + '"]') as HTMLElement;
      prev_ele?.focus();
      //prev_ele.scrollIntoView({ inline: 'nearest', block: 'end' })
      prev_ele.scrollIntoView({ behavior: 'smooth', block: 'nearest' })
    }
  }


  public static focus_first_element(table_ref: HTMLElement) {
    setTimeout((x:any) => {
      let first_ele = table_ref?.querySelector('.tbody .tr .td') as HTMLElement
      if (first_ele) {
        //console.log(this.get_next_unhidden(first_ele));
        this.get_next_unhidden(first_ele)?.focus();
      }
    }, 110)
  }

  private static get_next_unhidden(ele: HTMLElement): any {
    if (ele.hasAttribute('col-index') && !ele.hasAttribute('hidden')) return ele;
    else return this.get_next_unhidden(ele.nextElementSibling as HTMLElement);
  }

  public static delete_cell_content(config: any, column_config: any, active_col_config: any, active_row_data: any) {
    if (config.editable_grid && active_col_config['IsEnable']) {
      if (active_col_config['control'] == 'm2help')
        active_row_data[active_col_config['ColumnName'] + '_value'] = null;
      active_row_data[active_col_config['ColumnName']] = null;

      if (active_col_config['IsMust']) this.validate_cell(active_col_config, active_row_data);

    }

  }

  public static validate_and_next(rows: any, event: any, config: any, col_config: any[], active_row_data: any, save_row: EventEmitter<any>, method: () => void, add_row: () => void) {

    if (this.validate_row(config, col_config, active_row_data)) {
      active_row_data['detect'] = method;
      if ((active_row_data['row_state'] == 'e' || active_row_data['row_state'] == 'n')) {
        save_row.emit(config.editable_grid && config.row_save ? active_row_data : null);
      }
      M2GridHelper.next_sibling(null, rows, add_row, config, col_config, active_row_data, event.target, true);
    }
  }

  public static validate_row(config: any, col_config: any[], active_row_data: any): Boolean {
    let error_cnt = 0;

    if (config.editable_grid) {

      col_config.filter(x => x['IsEnable']).forEach(col => {
        if (col['IsMust'] && !active_row_data[col['ColumnName']]) {
          active_row_data[col['ColumnName'] + '_syserror'] = 'Field is required!!';
          error_cnt += 1;
        } else {
          if (col['m2help']) delete active_row_data[col['ColumnName'] + '_value_syserror']
          delete active_row_data[col['ColumnName'] + '_syserror']
        }
      })
    }
    active_row_data['in-valid'] = (error_cnt > 0 ? true : false);
    return (error_cnt > 0 ? false : true);
  }

  public static validate_cell(active_col_config: any, active_row_data: any) {
    if (active_col_config['IsMust'] && !active_row_data[active_col_config['ColumnName']]) {
      active_row_data['in-valid'] = true;
      active_row_data[active_col_config['ColumnName'] + '_syserror'] = 'Field is Required!!'
    }
    else {
      if (active_col_config['m2help']) delete active_row_data[active_col_config['ColumnName'] + '_value_syserror']
      delete active_row_data[active_col_config['ColumnName'] + '_syserror']
    }
  }


  public static handle_other_keydown(event: any, config: any, column_config: any, active_col_config: any, active_row_data: any, custom_events: EventEmitter<any>) {

    //#region "custom button SHORTCUT KEY event"
    let col_config;
    if (event.key != 'Alt') col_config = column_config.find((x :any) => x['shortcut_key'] && x['shortcut_key'].toLowerCase() == event.key.toLowerCase())
    let condition1 = event.altKey && col_config;
    let condition2 = active_col_config.control == 'button' && event.key == ' '
    if (condition1 || condition2) {
      event.preventDefault();
      let obj = { config: (condition2 ? active_col_config : col_config), data: active_row_data }
      custom_events.emit(obj);
      return;
    }
    //#endregion


  }


  public static set_viewport_item(data: any, viewport_item: any) {
    data.filter((x: any) => !x['hide'] && !x['filter_hide']).forEach((obj: any, index: any) => {
      if (index == 0) obj['active'] = true;
      if (index <= viewport_item) obj['viewport'] = true;
      else obj['viewport'] = false;
    })
  }


  public static column_toggle(event: KeyboardEvent, visible: any) {
    if (event.ctrlKey && (event.key == 'q' || event.key == 'Q')) {
      event.preventDefault();
      visible = !visible;
    }
    return visible;
  }

  public static emit_grid_key_events(event: any, config: any, key_events: EventEmitter<any>) {
    return new Promise((resolve) => {
      if (config.grid_key_events && config.grid_key_events.length > 0 && config.grid_key_events.includes(event.key.toLowerCase())) {
        event.preventDefault();
        event.stopPropagation();
        key_events.emit(event);
        resolve(true);
      } else if (config.grid_key_events_cntrl && config.grid_key_events_cntrl.length > 0 && event.ctrlKey && config.grid_key_events_cntrl.includes(event.key.toLowerCase())) {
        event.preventDefault();
        event.stopPropagation();
        key_events.emit(event);
        resolve(true);
      }
      resolve(false);
    });
  }

  public static row_state(column_config: any[], og_active_row_data:any, active_row_data: any) {
    if (active_row_data['row_state'] != 'n' && active_row_data['row_state'] != 'e')
      active_row_data['row_state'] = column_config.find(x => og_active_row_data[x['ColumnName']] != active_row_data[x['ColumnName']]) ? 'e' : null
  }

}
