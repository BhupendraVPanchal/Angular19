import { CdkDragRelease } from '@angular/cdk/drag-drop';
import { AfterViewInit, ChangeDetectionStrategy, ChangeDetectorRef, Component, ElementRef, EventEmitter, HostListener, Input, OnChanges, OnDestroy, OnInit, Output, Renderer2, SimpleChanges, ViewChild, ViewEncapsulation } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import './../../extension/sort.extension';
import { M2GridHelper } from './m2-grid-helper';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { MaterialModule } from '../../material/material.module';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'm2-grid',
  standalone: true,
  imports: [MaterialModule, CommonModule],
  templateUrl: './m2-grid-div.component.html',
  styleUrls: ['./m2-grid-div.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class M2GridDivComponent implements OnInit, AfterViewInit, OnChanges, OnDestroy {

  @Input() mtid: string;
  @Input() mt_disabled: any = false;
  @Input() config: grid_config;
  @Input() column_config: any[];
  @Input() data: any[] = [];

  @Input() form_data?: any;
  @Input() context_menu: any[] = [];
  @Input() form?: FormGroup;

  @Output() add_row: EventEmitter<any> = new EventEmitter<any>();
  @Output() save_row: EventEmitter<any> = new EventEmitter<any>();
  @Output() active_row: EventEmitter<any> = new EventEmitter<any>();

  @Output() cell_value_change: EventEmitter<any> = new EventEmitter<any>();

  @Output() custom_events: EventEmitter<any> = new EventEmitter<any>();
  @Output() grid_key_events: EventEmitter<any> = new EventEmitter<any>();

  @Output() context_menu_click: EventEmitter<any> = new EventEmitter<any>();


  all_visible: boolean = false;

  //@ViewChild('table') table_ref!: ElementRef;
  @ViewChild('table', { static: false }) table_ref!: ElementRef;
  public table: HTMLElement;

  focused_cell: { prev_row_idx: number, prev_col_idx: number, row_idx: number, col_idx: number } = { prev_row_idx: null, prev_col_idx: null, row_idx: -1, col_idx: -1 };

  @ViewChild('viewport') viewport: ElementRef;
  @ViewChild('header') header: ElementRef;
  @ViewChild('content') rows: ElementRef;
  @ViewChild('footer') footer: ElementRef;

  selected_radio_value: any = null;
  tableId: string;

  constructor(private route: ActivatedRoute, private dialog: MatDialog, private cdr: ChangeDetectorRef) {
    this.tableId = 'mtGrid_' + Math.random().toString(36).substr(2, 9);
    console.log(this.tableId);

  }

  ngOnInit() {

    this.calculate_dimensions();
    if (this.config.focus_on_load) M2GridHelper.focus_first_element(this.table_ref?.nativeElement);
    setTimeout((x: any) => {
      //if (this.config.allow_add && this.data.filter(x => !x['hide']).length == 0) this.utils.add_new_row(this.form, this.config.section, this.form_data, true);
      if (this.config.focus_on_load) M2GridHelper.focus_first_element(this.table_ref?.nativeElement);
      this.cdr.detectChanges();
    }, 50)
  }

  custom_grid: boolean = true

  ngAfterViewInit() { }

  viewport_item: number = 30
  ngOnChanges(changes: SimpleChanges) {
    if (changes['data'] && this.data) {
      M2GridHelper.set_viewport_item(this.data, this.viewport_item);
      this.og_active_row_data = this.data.filter(x => !x['hide'] && !x['filter_hide'])[0] ? JSON.parse(JSON.stringify(this.data.filter(x => !x['hide'] && !x['filter_hide'])[0])) : null;
      this.focused_cell = { prev_row_idx: this.focused_cell.row_idx, prev_col_idx: this.focused_cell.col_idx, row_idx: -1, col_idx: -1 };
      this.active_row_data = null;
      if (this.header) this.header.nativeElement.scrollLeft = 0;
      if (this.footer) this.footer.nativeElement.scrollLeft = 0;

      if (this.context_menu && this.column_config) this.bind_context_menu();
    }

    setTimeout((x: any) => { this.cdr.detectChanges() });
  }

  bind_context_menu() {
    let ActionType = 'default'
    let obj = this.column_config.filter(x => x['control'] == 'button').map(({ ColumnCaption: Caption, ColumnName: Name }) => ({ ActionType, Caption, Name }))
    if (this.context_menu) this.context_menu = [...obj, ...this.context_menu];
    else this.context_menu = obj;
  }

  get viewport_height() {
    if (this.viewport) {
      let height = (this.header.nativeElement as HTMLElement).getBoundingClientRect().height + (this.footer ? (this.footer.nativeElement as HTMLElement).getBoundingClientRect().height : 0);
      return `calc(100% + -${height}px)`;
    }
    else return '100%';
  }

  add_item() { this.add_row.emit(); }


  public set_focus(row_idx: Number, col_idx: Number) { M2GridHelper.focus_cell(this.rows, row_idx, col_idx); };

  public first_visible_focus() { M2GridHelper.focus_first_element(this.table_ref?.nativeElement); };

  //#region "table helper"

  //#region "table navigation"
  og_row_data: any = {};
  og_active_row_data: any = {};
  og_active_row_data_cell: any = {};
  active_row_data: any = {};
  active_col_config: any = {};

  //prev_active_row_data: any = {};
  //prev_active_col_config: any = {};

  block_events(event: any): boolean {
    if (this.mt_disabled || this.mt_disabled == 1) {
      event?.preventDefault();
      event?.stopPropagation();
      event?.stopImmediatePropagation();
      return true;
    } else return false;
  }

  @HostListener('keydown', ['$event'])
  onKeydown(event: KeyboardEvent) {
    // Ensure the event is scoped to this instance
    if (this.isEventForThisGrid(event)) {
      this.keydown(event);
    }
  }

  private isEventForThisGrid(event: Event): boolean {
    return (event.target as HTMLElement).closest(`#${this.tableId}`) !== null;
  }


  pass_events: any = ['ArrowUp', 'ArrowDown', 'ArrowLeft', 'ArrowRight', 'Enter', 'Tab', 'Delete'];

  async keydown(event: KeyboardEvent) {


    if (this.active_row_data && this.active_row_data[this.active_col_config['ColumnName'] + '_edit']) return;

    this.all_visible = M2GridHelper.column_toggle(event, this.all_visible);

    if (await M2GridHelper.emit_grid_key_events(event, this.config, this.grid_key_events)) {
      this.keys = '';
      return;
    }

    if ((event.target as HTMLElement).classList.contains('td')) {

      switch (event.key) {
        case 'ArrowUp':
          M2GridHelper.prev_sibling(this.config, this.column_config, this.active_row_data, event.target as HTMLElement, true);
          M2GridHelper.break_event(event);
          this.cdr.detectChanges();
          break;
        case 'ArrowDown':

          M2GridHelper.next_sibling(this.viewport.nativeElement, this.rows.nativeElement, null, this.config, this.column_config, this.active_row_data, event.target as HTMLElement, true);
          M2GridHelper.break_event(event);
          this.cdr.detectChanges();
          break;
        case 'ArrowLeft':
          M2GridHelper.prev_sibling(this.config, this.column_config, this.active_row_data, event.target as HTMLElement, false);
          M2GridHelper.break_event(event);
          this.cdr.detectChanges();
          break;
        case 'ArrowRight':
          M2GridHelper.next_sibling(this.viewport.nativeElement, this.rows.nativeElement, null, this.config, this.column_config, this.active_row_data, event.target as HTMLElement, false);
          M2GridHelper.break_event(event);
          this.cdr.detectChanges();
          break;
        case 'Delete':
          M2GridHelper.delete_cell_content(this.config, this.column_config, this.active_col_config, this.active_row_data);
          M2GridHelper.break_event(event);
          this.cdr.detectChanges();
          break;
        case 'Enter':
          //if (this.config.editable_grid) {
          M2GridHelper.validate_and_next(this.rows.nativeElement, event, this.config, this.column_config, this.active_row_data, this.save_row, this.detect.bind(this), null);
          M2GridHelper.break_event(event);
          this.cdr.detectChanges();
          //}
          break;
        case 'Escape':
          if (!this.active_row_data['save_state'] && this.config.editable_grid && this.og_active_row_data && Object.keys(this.og_active_row_data).length > 0) {
            let data = this.data.filter(x => !x['hide'] && !x['filter_hide']);

            //Object.keys(this.og_active_row_data).forEach(key => {
            //  data[this.focused_cell.row_idx][key] = this.og_active_row_data[key];
            //  delete data[this.focused_cell.row_idx][key + '_error'];
            //  delete data[this.focused_cell.row_idx]['in-valid'];
            //});

            this.column_config.forEach(col => {
              data[this.focused_cell.row_idx][col.ColumnName] = this.og_active_row_data[col.ColumnName];
              delete data[this.focused_cell.row_idx][col.ColumnName + '_syserror'];
            })
            delete data[this.focused_cell.row_idx]['in-valid'];

            if (this.og_active_row_data['row_state'] != 'n') M2GridHelper.validate_row(this.config, this.column_config, this.active_row_data);

            this.cdr.detectChanges();
            M2GridHelper.break_event(event);
          }
          break;
        default:

          //#region "custom button SHORTCUT KEY event"
          let config;
          if (event.key != 'Alt') config = this.column_config.find(x => x['shortcut_key'] && x['shortcut_key'].toLowerCase() == event.key.toLowerCase())
          let condition1 = event.altKey && config;
          let condition2 = this.active_col_config.control == 'button' && event.key == ' '
          if (condition1 || condition2) {
            M2GridHelper.break_event(event);
            let obj = { config: condition2 ? this.active_col_config : config, data: this.active_row_data }
            this.custom_events.emit(obj);
            return;
          }
          //#endregion

          if (event.key == ' ' && event.ctrlKey) {
            this.bind_row_edit_modal(event); return;
          }
          //else if (event.key == ' ' && !this.config.allow_edit && !this.config.allow_add) {

          if (event.key == ' ' && !this.config.allow_edit && !this.config.allow_add) {
            this.active_row_data['mt-selected'] = !this.active_row_data['mt-selected'];
            M2GridHelper.break_event(event);
            return;
          }
          else {

            if (event.altKey && event.key.toLowerCase() == 's') {
              M2GridHelper.break_event(event);
              this.sort_click(this.active_col_config, this.active_col_config['ColumnName']);
            }
            else if (this.config.column_search && event.altKey && event.key.toLowerCase() == 'f') {
              M2GridHelper.break_event(event);
              if (!this.active_row_data['in-valid']) {
                let search_bar = (this.header.nativeElement as HTMLElement).querySelector('[row-index="2"]') as HTMLElement;
                (search_bar.querySelector('[col-index="' + this.active_col_config['ColumnOrder'] + '"] input') as HTMLElement).focus();
              }

            }
            else {

              if (this.active_col_config['IsEnable'] && !this.active_row_data[this.active_col_config['ColumnName'] + '_disabled'])
                this.enable_edit_control(event);
              else if (this.active_row_data[this.active_col_config['ColumnName'] + '_mtkey'] == event.key) {
                let obj = { key: event.key, config: this.active_col_config, data: this.active_row_data }
                M2GridHelper.break_event(event);
                this.custom_events.emit(obj);
              }
              else if (event.key == ' ') M2GridHelper.break_event(event);

            }
          }

          //}
          break;
      }
    }
  }

  public detect() { setTimeout((x: any) => { this.cdr.detectChanges(); }, 10) }

  estimated_content_height = 380
  calculate_dimensions() {
    if (this.data) this.estimated_content_height = (this.data.filter(x => !x['hide']).length * this.item_height);
  }

  custom_button_click(col: any, item: any) {
    let obj = { config: col, data: item }
    this.custom_events.emit(obj);
  }

  click(event: any) { /*M2GridHelper.validate_row(this.config, this.column_config, this.active_row_data);*/ }

  focus(event: any) {

    if (this.isEventForThisGrid(event)) {
      //this.keydown(event);
    } else {
      return;
    }
    if (this.prev_active_element) {
      //(this.prev_active_element.closest('.tr') as HTMLElement).classList.remove('focus-row');
      //const trElement = this.prev_active_element.closest('.tr') as HTMLElement;
      const trElement = this.prev_active_element as HTMLElement;
      if (!trElement) {
        
      }

      const focusRows = document.querySelectorAll(`#${this.tableId} .focus-row`);
      // Remove the 'focus-row' class from each element
      focusRows.forEach(row => row.classList.remove('focus-row'));

      // Ensure that the .tr element belongs to the current M2Grid instance
      if (trElement && trElement.closest(`#${this.tableId}`)) {
        //trElement.classList.remove('focus-row');
        
      }
     
      this.prev_active_element.classList.remove('focus-cell');
      this.active_col_config['cell_begin_edit_triggered'] = false;
    }


    if (this.block_events(event)) return;
    this.keys = '';

    let row_index = Number((event.target as HTMLElement).getAttribute('row-index'));
    if (this.og_active_row_data && this.active_row_data && row_index == this.focused_cell.row_idx) M2GridHelper.row_state(this.column_config, this.og_active_row_data, this.active_row_data);

    if (this.active_row_data && this.active_row_data['in-valid'] && row_index != this.focused_cell.row_idx) {
      M2GridHelper.validate_row(this.config, this.column_config, this.active_row_data);
      if (this.active_row_data['in-valid']) M2GridHelper.focus_cell(this.rows, this.focused_cell.row_idx, this.focused_cell.col_idx);
      return;
    }

    //this.prev_active_row_data = this.active_row_data ? JSON.parse(JSON.stringify(this.active_row_data)) : null;
    //this.prev_active_col_config = this.active_col_config ? JSON.parse(JSON.stringify(this.active_col_config)) : null;

    if (this.active_row_data) delete this.active_row_data[this.active_col_config['ColumnName'] + '_edit']; //remove if any existing edit is pending did for sorti click issue

    this.active_row_data = this.data[row_index];
    this.active_col_config = this.column_config.find(x => x['ColumnOrder'] == (event.target as HTMLElement).getAttribute('col-index'));



    const active_cell = (event.target as HTMLElement);

    const row_idx = (active_cell.closest('.tr') as HTMLElement).getAttribute('row-index');
    const col_idx = Array.from((active_cell.closest('.tr') as HTMLElement).children).indexOf(active_cell);
    active_cell.classList.add('focus-cell');
    if ((active_cell.closest('.tr') as HTMLElement)) {
      (active_cell.closest('.tr') as HTMLElement).classList.add('focus-row');
    }

    if (row_index != this.focused_cell.row_idx) {
      this.og_active_row_data = this.active_row_data ? JSON.parse(JSON.stringify(this.active_row_data)) : null;
      if (!this.config.multi_row_select) this.active_row.emit({ activ_cell: this.focused_cell, data: this.active_row_data });
    }
    this.og_active_row_data_cell = this.active_row_data ? JSON.parse(JSON.stringify(this.active_row_data)) : null;

    this.focused_cell.prev_row_idx = this.focused_cell.row_idx;
    this.focused_cell.prev_col_idx = this.focused_cell.col_idx;
    this.focused_cell.row_idx = Number(row_idx);
    this.focused_cell.col_idx = Number(col_idx);

    //if (!this.config.multi_row_select) this.active_row.emit({ activ_cell: this.focused_cell, data: this.active_row_data });

  }

  prev_active_element: HTMLElement;
  blur1(event: any) {
    const blured_cell = event.target as HTMLElement;
    const row_idx = (blured_cell.closest('.tr') as HTMLElement).getAttribute('row-index');
    const col_idx = Array.from((blured_cell.closest('.tr') as HTMLElement).children).indexOf(blured_cell);

    if (this.active_row_data && this.active_row_data['in-valid']) this.prev_active_element = blured_cell;

    if ((blured_cell.closest('.tr') as HTMLElement) && (blured_cell.closest('.tr') as HTMLElement).closest(`#${this.tableId}`)) {
      //trElement.classList.remove('focus-row');
      (blured_cell).classList.remove('focus-row');
    }

    //blured_cell.classList.remove('focus-cell');
  }

  blur(event: any) {
    const blured_cell = event.target as HTMLElement;

    const row_idx = (blured_cell.closest('.tr') as HTMLElement).getAttribute('row-index');
    const col_idx = Array.from((blured_cell.closest('.tr') as HTMLElement).children).indexOf(blured_cell);

    //if (this.active_row_data && this.active_row_data['in-valid']) this.prev_active_element = blured_cell;
    this.prev_active_element = blured_cell;

    // prev active cell manage concept
    //(blured_cell.closest('.tr') as HTMLElement).classList.remove('focus-row');
    blured_cell.classList.remove('focus-cell');
  }


  clear_focus() { if (this.active_row_data) delete this.active_row_data[this.active_col_config['ColumnName'] + '_edit']; }

  //#endregion

  private cellBeginEditSubscription: Subscription | undefined;

  //#region "control related"
  keys: string = '';
  async enable_edit_control($event: any) {

    //const callback = this.cell_begin_edit.invoke($event);
    //callback.then(async x => {

    if (this.is_keypress_event($event.keyCode)) this.keys = this.keys.trim() + $event.key

    if (!this.active_row_data[this.active_col_config['ColumnName'] + '_edit']) {
      this.active_row_data['save_state'] = ''
      //await this.cell_begin_edit.invoke($event);
    }

    if (this.config.editable_grid && this.config.allow_edit) {

      let element = $event.target
      switch (this.active_col_config['control']) {
        case 'textbox':
        case 'date':
          this.bind_textbox(element, $event)
          break;
        case 'checkbox':
          this.bind_checkbox(element, $event);
          break;
        case 'm2help':
          if (!$event.ctrlkey && $event.key == ' ') this.bind_m2help(element, $event);
          break;
        case 'dropdownlist':
          this.bind_dropdownlist(element, $event);
          break;
        case 'radiobutton':
          this.bind_radiobutton(element, $event);
          break;
        default:
          if ($event.key == ' ') M2GridHelper.break_event($event);
          break;
      }
    } else {
      if ($event.key == ' ') M2GridHelper.break_event($event);
    }

    //});

  }


  //#region "textbox related"
  bind_textbox(element: any, _event: any) {
    if (!this.active_row_data[this.active_col_config['ColumnName'] + '_edit']) {

      //#region "f2 handler"
      if (_event.key == 'F2') {
        this.active_row_data[this.active_col_config['ColumnName'] + '_edit'] = true;

        setTimeout((x: any) => {
          if (element && element.querySelector('input')) {
            element.querySelector('input').focus();

            //#region "handle keydown"
            element.querySelector('input').addEventListener('keydown', (input_event: KeyboardEvent) => {

              if (input_event.key == 'Escape') {
                this.active_row_data[this.active_col_config['ColumnName'] + '_edit'] = false;
                this.active_row_data[this.active_col_config['ColumnName']] = this.og_active_row_data_cell[this.active_col_config['ColumnName']];

                M2GridHelper.validate_cell(this.active_col_config, this.active_row_data);

                this.cdr.detectChanges();
                (_event.target as HTMLElement).focus();
                M2GridHelper.break_event(input_event);
              }
              else if (input_event.key == 'Enter' || input_event.key == 'Tab') {
                this.active_row_data[this.active_col_config['ColumnName'] + '_edit'] = false;

                M2GridHelper.validate_cell(this.active_col_config, this.active_row_data);

                this.cdr.detectChanges();
                (_event.target as HTMLElement).focus();
                M2GridHelper.break_event(input_event);
              }

            })
            //#endregion
          }
        });
        this.cdr.detectChanges();
      }
      //#endregion

      //#region "keypress for key manage"
      if (this.is_keypress_event(_event.keyCode)) {

        if (!_event.ctrlKey && _event.key != 'Enter' && !this.active_row_data[this.active_col_config['ColumnName'] + '_edit']) {
          M2GridHelper.break_event(_event);

          this.active_row_data[this.active_col_config['ColumnName'] + '_edit'] = true;
          this.cdr.detectChanges();

          //#region "set_input"
          setTimeout((x: any) => {
            if (element && element.querySelector('input')) {
              element.querySelector('input').focus();

              setTimeout((x: any) => { if (_event.key != 'F2' && _event.key != ' ') this.bind_values_to_control(element, _event); });
              //#region "handle keydown"
              element.querySelector('input').addEventListener('keydown', (input_event: KeyboardEvent) => {
                if (input_event.key == 'Escape') {
                  this.active_row_data[this.active_col_config['ColumnName'] + '_edit'] = false;
                  this.active_row_data[this.active_col_config['ColumnName']] = this.og_active_row_data_cell[this.active_col_config['ColumnName']];

                  M2GridHelper.validate_cell(this.active_col_config, this.active_row_data);

                  this.cdr.detectChanges();
                  (_event.target as HTMLElement).focus();
                  M2GridHelper.break_event(input_event);
                }
                else if (input_event.key == 'Enter' || input_event.key == 'Tab') {
                  this.active_row_data[this.active_col_config['ColumnName'] + '_edit'] = false;

                  M2GridHelper.validate_cell(this.active_col_config, this.active_row_data);

                  this.cdr.detectChanges();
                  (_event.target as HTMLElement).focus();
                  M2GridHelper.break_event(input_event);
                }

              })
              //#endregion

            }
          }, 10);
          //#endregion

        }


      };
      //#endregion
    }

  }

  bind_values_to_control(element: any, $event: any) {
    if (this.active_col_config['control'] == 'textbox' && this.active_col_config['key_press_regex']) {
      if (new RegExp(this.active_col_config['key_press_regex']['regex']).test(this.keys)) {
        element.querySelector('input').value = this.keys;
        this.active_row_data[this.active_col_config['ColumnName']] = element.querySelector('input').value;
      } else {
        element.querySelector('input').focus();
      }
    } else {
      if (this.active_col_config['control'] == 'date') {
        (element.querySelector('input') as HTMLInputElement).value = this.keys;
      }
      if (this.active_col_config['control'] == 'textbox') {
        element.querySelector('input').value = this.keys;
        this.active_row_data[this.active_col_config['ColumnName']] = element.querySelector('input').value;
      }
    }
    this.keys = '';
  }

  is_keypress_event(keyCode: number): boolean {
    const isCharacterKey = (keyCode >= 48 && keyCode <= 90) || (keyCode >= 96 && keyCode <= 111) || (keyCode >= 186 && keyCode <= 222);
    const isSpaceKey = keyCode === 32;
    return isCharacterKey || isSpaceKey;
  }

  //#endregion

  bind_checkbox(element: any, event: any) {
    if (event.key == ' ') {
      M2GridHelper.break_event(event);
      if (this.active_col_config['IsEnable']) this.active_row_data[this.active_col_config['ColumnName']] = !this.active_row_data[this.active_col_config['ColumnName']];
      M2GridHelper.row_state(this.column_config, this.og_active_row_data, this.active_row_data);

      this.emit_cell_value_change();

      this.cdr.detectChanges();
    }
  }

  bind_m2help(element: any, event: any) {
    M2GridHelper.break_event(event);
    if (this.active_col_config['IsEnable']) {
      element.blur();
      let config = new MatDialogConfig();
      config = {
        data: {
          config: this.active_col_config['MTHelpConfig'],
          field: this.active_col_config['ColumnName'],
          form_data_obj: this.form_data,
          form: this.form,
          row_data_obj: this.active_row_data
        },
        panelClass: "mid-width"
      }

      //const dialog_close = this.dialog.open(MtListModalComponent, config);
      //dialog_close.afterClosed().subscribe(res => {
      //  if (res && res.data != undefined) {
      //    let val_col = this.active_col_config['ColumnName'] + '_value';
      //    let display_col = this.active_col_config['ColumnName'];
      //    let return_col_mapp = res.data.return_column_mapping;
      //    let data_source = res.data.return_mapping_data_source;

      //    if (res.data.selected_options.length > 1) {
      //      //For multiselect in grid
      //      (data_source as any[]).forEach((x, i) => {
      //        if (i == 0) {
      //          this.utils.set_return_mapping(return_col_mapp, x, this.active_row_data);
      //        }
      //        else {
      //          if (this.data.findIndex(c => c && c[display_col] == x.display) < 0) {
      //            if (this.data.find(c => c && c[display_col] == null)) {
      //              this.utils.set_return_mapping(return_col_mapp, x, (this.data as any[]).find(c => c[display_col] == null));
      //            } else {
      //              this.utils.add_new_row(this.form, this.config.section, this.form_data, true);
      //              this.utils.set_return_mapping(return_col_mapp, x, (this.data as any[]).find(c => c[display_col] == null));
      //            }
      //          }
      //        }
      //      });

      //    } else {
      //      this.active_row_data[display_col] = res.data.selected_comma_seperated_display
      //      this.active_row_data[val_col] = res.data.selected_comma_seperated_value
      //    }

      //    this.emit_cell_value_change();
      //  }
      //  M2GridHelper.validate_cell(this.active_col_config, this.active_row_data);
      //  element.focus();
      //  this.cdr.detectChanges();

      //});

    }

  }

  bind_dropdownlist(element: any, event: any) {
    M2GridHelper.break_event(event);
    if ((event.key == ' ' || event.key == 'F2') && !this.active_row_data[this.active_col_config['ColumnName'] + '_edit']) {
      this.active_row_data[this.active_col_config['ColumnName'] + '_edit'] = true;
      this.cdr.detectChanges();
      setTimeout((x: any) => {
        let ele = element.querySelector('mat-select') || element.querySelector('select') as HTMLElement
        ele?.focus();

        //#region "handle keydown"
        ele.addEventListener('keydown', (select_event: KeyboardEvent) => {
          if (select_event.key == 'Escape') {
            this.active_row_data[this.active_col_config['ColumnName'] + '_edit'] = false;
            this.active_row_data[this.active_col_config['ColumnName']] = this.og_active_row_data_cell[this.active_col_config['ColumnName']];

            M2GridHelper.validate_cell(this.active_col_config, this.active_row_data);

            this.cdr.detectChanges();
            (event.target as HTMLElement).focus();
            M2GridHelper.break_event(select_event);
          }
          else if (select_event.key == 'Enter' || select_event.key == 'Tab') {
            this.active_row_data[this.active_col_config['ColumnName'] + '_edit'] = false;

            M2GridHelper.validate_cell(this.active_col_config, this.active_row_data);

            this.cdr.detectChanges();
            (event.target as HTMLElement).focus();
            M2GridHelper.break_event(select_event);
          }
        })
        //#endregion

      })
    }
  }

  bind_radiobutton(element: any, event: any) {
    if (event.key == ' ') {
      M2GridHelper.break_event(event);
      if (this.active_col_config['IsEnable']) {
        this.selected_radio_value = this.active_row_data[this.active_col_config['ColumnName']];
        this.emit_cell_value_change();
        this.cdr.detectChanges();
      }
    }
  }

  checkbox_click($event: any, obj: any, col: any) {

    (obj[col] = !obj[col])
    this.emit_cell_value_change();

    this.blur($event);
  }

  radio_click($event: any, obj: any, col: any) {
    this.selected_radio_value = obj[col];
    this.emit_cell_value_change();
  }

  change_cell_value($event: any) { this.emit_cell_value_change(); }

  emit_cell_value_change() { this.cell_value_change.emit({ column_config: this.active_col_config, prev_data: this.og_active_row_data, current_data: this.active_row_data, detect: this.detect.bind(this) }); }

  bind_row_edit_modal(event: any) {
    M2GridHelper.break_event(event);
    let config = new MatDialogConfig();
    config = {
      data: {
        column_config: this.column_config,
        form_data_obj: this.form_data,
        form: this.form,
        row_data_obj: this.active_row_data,
        section: this.config.section
      },
      panelClass: "mid-width"
    }

    let prev_data = JSON.parse(JSON.stringify(this.active_row_data));
    //const dialog_close = this.dialog.open(RowEditModalComponent, config);
    //dialog_close.afterClosed().subscribe(res => {
    //  if (res) {
    //    if (res.event == 'save') {
    //      M2GridHelper.validate_row(this.config, this.column_config, this.active_row_data);
    //      if (this.config.row_save) this.save_row.emit(res.data);
    //    }
    //    if (res.event == 'close')
    //      Object.keys(prev_data).forEach(key => { this.data.filter(x => !x['hide'] && !x['filter_hide'])[this.focused_cell.row_idx][key] = prev_data[key]; });
    //  }
    //});

  }

  //#endregion



  track(idx: number) { return idx; };

  //#region "table column resize"
  start_x: number;
  start_width: number;
  column: HTMLElement;

  drag_started: boolean = false;

  drag_start(event: any, col: HTMLElement) {
    this.drag_started = true;
    this.start_x = event.event['clientX'];
    this.start_width = col.offsetWidth;
    this.column = col;
  }

  drag_moved(event: any, col: HTMLElement) {

    if (this.drag_started) {
      col.classList.add('active_drag');
      let drag_bar: HTMLElement = col.querySelector('.column-drag-line');
      drag_bar.style.height = this.table.getBoundingClientRect().height + 'px';
    }

  }

  drag_release(event: CdkDragRelease, col: HTMLElement, col_config: any) {

    const width = this.start_width + ((event.event as any)['clientX'] - this.start_x);
    //this.column.style.width = width + 'px';
    //col.style.width = width + 'px';
    col_config.width = width;

    this.table.style.width = (parseInt(this.table.style.width) + (width - this.start_width)) + 'px';
    event.source._dragRef.reset();
    col.classList.remove('active_drag');
    let drag_bar: HTMLElement = col.querySelector('.column-drag-line');
    drag_bar.style.removeProperty('height');
    this.drag_started = false;
    event.source.reset();
    event.source._dragRef.reset();
    this.cdr.detectChanges();
  }

  //#endregion

  //#endregion

  query: any = {}

  sort_click(config: any, name: string) {
    if (this.active_row_data && this.active_row_data['in-valid']) {
      M2GridHelper.focus_cell(this.rows, this.focused_cell.row_idx, this.focused_cell.col_idx);
      return;
    }
    if (config.sorting) {

      this.clear_focus();
      this.viewport.nativeElement.scrollTop = 0;

      this.query.sort_col_name = name;
      this.query.sort_type = this.query.sort_type === 'asc' ? 'desc' : 'asc';
      let type = 'string';
      switch (config.control) {
        case 'date': case 'datetime': type = 'date'; break;
        case 'checkbox': type = 'boolean'; break;
        case 'textbox': type = config.key_press?.toLowerCase() == 'numericonly' || config.key_press?.toLowerCase() == 'decimalnumeric' ? 'number' : 'string'; break;
        default: type = 'string'; break;
      }
      console.log("this.query.sort_col_name", this.query.sort_col_name);
      console.log("this.query.sort_type", this.query.sort_type);
      console.log("type", type);
      this.data = this.data.sortByKey(type, this.query.sort_col_name, this.query.sort_type);
      this.data.filter(x => !x['hide'] && !x['filter_hide']).forEach((x, idx) => { x['viewport'] = idx >= 0 && idx <= this.viewport_item; });

      setTimeout((x: any) => { M2GridHelper.focus_cell(this.rows, 0, this.active_col_config['ColumnOrder']); });

    }

  }

  is_sorting(name: string) { return this.query.sort_col_name == name; };

  is_sort(sort_type: sort_type, name: string) { return this.query.sort_type === sort_type && this.query.sort_col_name === name };

  item_height: number = 28;

  get scroll_bar_width(): number {
    if (this.viewport) {
      let scroll_width = 0;
      scroll_width = this.viewport.nativeElement.offsetWidth - this.viewport.nativeElement.clientWidth;
      return scroll_width;
    }
    else return 7.5;
  }

  head_scroll($event: any) { this.viewport.nativeElement.scrollLeft = $event.target.scrollLeft }

  foot_scroll($event: any) { this.viewport.nativeElement.scrollLeft = $event.target.scrollLeft }

  //scroll($event) {
  //  $event.preventDefault();
  //  $event.stopPropagation();

  //  this.header.nativeElement.scrollLeft = $event.target.scrollLeft || 0;
  //  if (this.footer) this.footer.nativeElement.scrollLeft = $event.target.scrollLeft || 0;

  //  if (this.data && this.data.length > 0) {
  //    const scroll_top = $event.target.scrollTop;
  //    const start = Math.floor(scroll_top / this.item_height);
  //    const end = Math.min((start + Math.ceil($event.target.clientHeight / this.item_height) + 5), this.data.filter(x => !x['hide'] && !x['filter_hide']).length);
  //    this.data.filter(x => !x['hide'] && !x['filter_hide']).forEach((x, idx) => { x['viewport'] = idx >= start && idx <= end; });
  //    let translateY = Math.max((start * this.item_height), 0);
  //    this.rows.nativeElement.style.transform = `translateY(${translateY}px)`;

  //  }
  //}

  row_buffer = 20
  scroll($event: any) {

    M2GridHelper.break_event($event);

    this.header.nativeElement.scrollLeft = $event.target.scrollLeft || 0;
    if (this.footer) this.footer.nativeElement.scrollLeft = $event.target.scrollLeft || 0;

    if (this.data && this.data.length > 0) {
      const scroll_top = $event.target.scrollTop;
      const start = Math.floor((scroll_top - this.row_buffer * this.item_height) / this.item_height);
      const end = Math.min((start + Math.ceil(($event.target.clientHeight + 2 * this.row_buffer * this.item_height) / this.item_height)), this.data.filter(x => !x['hide'] && !x['filter_hide']).length);

      this.data.filter(x => !x['hide'] && !x['filter_hide']).forEach((x, idx) => { x['viewport'] = idx >= start && idx <= end; });

      let translateY = Math.max((start * this.item_height), 0);
      this.rows.nativeElement.style.transform = `translateY(${translateY}px)`;
    }
  }


  adjust_scroll_position(element: any) { }

  search_keydown($event: any) {
    if ($event.altKey && $event.key.toLowerCase() == 'd') {
      M2GridHelper.break_event($event);
      if ((this.rows.nativeElement as HTMLElement).querySelector('.tr')) {
        let row = (this.rows.nativeElement as HTMLElement).querySelector('.tr');
        if (row) (row.querySelector('[col-index="' + ($event.target as HTMLElement).getAttribute('col-index') + '"]') as HTMLElement).focus();
      }
    }
  }

  timer: any;
  search_input($event: any) {
    clearTimeout(this.timer);
    this.timer = setTimeout(() => { this.filter_data($event); }, 1000)
  }

  filter_data($event: any) {
    this.viewport.nativeElement.scrollTop = 0;
    let filters = this.column_config.filter(x => x['mt_filter']).map(item => ({ column: item.ColumnName, text: item.mt_filter, operator: 'contains' }));
    let filter_functions = filters.map(this.filter_function);
    this.data.filter(x => !x['hide']).forEach(item => {
      if (!filter_functions.every(filter => filter(item))) item['filter_hide'] = true;
      else item['filter_hide'] = false;
    });

    M2GridHelper.set_viewport_item(this.data, this.viewport_item);
    this.cdr.detectChanges();
  }

  filter_function(filter: any): (item: any) => boolean {
    return function (item: any): boolean {
      const value = item[filter.column] ? item[filter.column].toString().toLowerCase() : null;
      const search_text = filter.text.toLowerCase();

      switch (filter.operator) {
        case 'contains':
          return value ? value.includes(search_text) : false;
        case 'startsWith':
          return value ? value.startsWith(search_text) : false;
        case 'endsWith':
          return value ? value.endsWith(search_text) : false;
        default:
          return false;
      }
    };
  }

  contextmenu($event: any) {
  }

  cmenu_click($event: any, obj: any) {
    if (obj['ActionType'] == 'default') this.custom_button_click(this.column_config.find(x => x['ColumnName'] == obj['Name']), this.active_row_data)
    else this.context_menu_click.emit(obj);
  }

  get check_context_menu() {
    if (this.context_menu && this.context_menu.filter(x => x['ActionType'] == 'default') && this.active_col_config && this.active_row_data) return this.context_menu.filter(x => x['ActionType'] == 'default').length
    else if (this.context_menu && this.context_menu.filter(x => x['ActionType'] != 'default')) return this.context_menu.length
    else return 0
  }

  ngOnDestroy() { }



}












type sort_type = | 'asc' | 'desc'


export interface grid_config {
  singleton: boolean;
  title: string;
  section: string;
  height: string;
  editable_grid: boolean;
  grid_key_events: string[];
  grid_key_events_cntrl: string[];
  allow_add: boolean;
  allow_edit: boolean;
  allow_delete: boolean;
  row_save: boolean;
  multi_row_select: boolean;
  full_width: boolean;
  column_search: boolean;
  column_aggregate: boolean;
  focus_on_load: boolean;
}

