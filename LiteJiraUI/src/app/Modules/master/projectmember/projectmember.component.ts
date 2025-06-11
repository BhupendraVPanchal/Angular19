import { AfterViewInit, Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonAlertsComponent } from '../../../shared/utils/alerts/common-alerts/common-alerts.component';
import { AlertService } from '../../../shared/utils/alerts/alert-service.service';
import { CommonService } from '../../../shared/services/common.service';
import { MatPaginator } from '@angular/material/paginator';
import { AlertType } from '../../../shared/utils/alerts/alert-types';
import { ProjectmemberAddEditComponent } from './projectmember-add-edit/projectmember-add-edit.component';
import { MasterService } from '../master.service';
import { DynamicAlertService } from '../../../shared/common/dynamic-alert/dynamic-alert.service';
import { NotificationService } from '../../../shared/services/notification.service';

@Component({
  selector: 'app-projectmember',
  standalone: false,
  templateUrl: './projectmember.component.html',
  styleUrl: './projectmember.component.scss'
})
export class ProjectmemberComponent implements OnInit, AfterViewInit {

  public _headertitle: string = 'Master';
  public column_config: any[] = [];
  public data_items: any[] = [];
  public data_count: number = 0;
  public entity_code: number = 0;
  public searched_text: string = '';

  public query: any;

  @ViewChild('master_paginator') paginator!: MatPaginator;
  @ViewChild('search_qmd_field') myInputField!: ElementRef;

  grid_config: any = {
    title: '<h5 class="mb-0">Project Member Master</h5>',
    grid_name: 'projectmember',
    is_addbtn: false,
    full_width: true,
    grid_class: '',
    data_count: 1000
  }

  table_info: any = {

    tablename: '[dbo].projectmember',
    primarykeycolumn: 'projectmemberid',
    primarykeyvalue: '0',
    fieldcolumn: 'is_locked',
    fieldvalue: '0'
  }

  constructor(
    private _dialog: MatDialog,
    private _router: Router,
    private _alertservice: AlertService,
    private _common_service: CommonService,
    private _masterserviceobj: MasterService,
    private _dynamicalertservice: DynamicAlertService,
    private _notificationservice: NotificationService,
  ) { }

  ngOnInit(): void {
    this.init();
    this.get_data();
  }


  ngAfterViewInit(): void {

  }

  public get_data() {
    this.get_master_data();
  }

  public init() {
    this.searched_text = '';
    this.query = {
      page_number: 1,
      page_size: 20,
      search_column: 'name',
      search_text: '',
      sort_col_name: 'projectmemberid',
      sort_type: 'Desc'
    };
    this.data_items = [];
  }

  public get_master_data(): void {
    this._masterserviceobj.get_companymaster_data(this.query)
      .subscribe({
        next: (RtnData) => {
          this.column_config = RtnData.Data[0].Table;
          this.data_items = RtnData.Data[1].Table1;
          this.data_count = RtnData.DataCount;
        },
        error: (err_response) => {
          this._alertservice.showAlert(err_response["error"]["message"], AlertType.BG_WARNING, false);
          this._router.navigate(['/home']);
        }
      });
  }

  public add_edit_record(_entity_code: string): void {
    let dialogRef = this._dialog.open(ProjectmemberAddEditComponent, { data: { entity_code: _entity_code }, panelClass: 'mid-width' });
    dialogRef.afterClosed().subscribe((res) => {
      this.get_master_data();
    });

  }

  pageChangeHandler(event: any): void {
    console.log(event);
    this.query.page_size = event.pageSize;
    this.query.page_number = event.pageIndex + 1;
    this.data_count = event.length;
    this.get_master_data();
  }

  typingTimer: any;
  doneTypingInterval: number = 200; // 200 milisecond 
  timerActive: boolean = false;

  searchChangeHandler(): void {
    clearTimeout(this.typingTimer);
    if (!this.timerActive) {
      this.typingTimer = setTimeout(() => {
        this.query.search_text = this.searched_text;
        // this.query.page_index = this.paginator.pageIndex = 0; 
        this.get_master_data();
      }, this.doneTypingInterval);
    }
  }

  public delete_record(_code: string): void {
    this._dynamicalertservice.fire({
      title: 'Do you want to delete this data ?',
      text: 'This process is irreversible.',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonText: 'yes',
      cancelButtonText: 'no'
    }).then((r) => {
      if (r && r.isConfirmed) {
        this._masterserviceobj.delete_companymaster({ companyid: _code }).subscribe({
          next: (data) => {
            this.data_items = this.data_items.filter((x) => {
              return !(x.companyid == _code)
            })
          }, error: (err_response) => {
            this._notificationservice.showToast(err_response['error']['message']);
          }
        })
      }
    })
  }


  export_to_excel(): void {

  }

  public custom_events(event: any) {
    console.log(event);
    let ColumnCaption = String(event?.action).trim().toLowerCase();
    if (ColumnCaption == 'edit') {
      this.add_edit_record(event.data.companyid);
    }
    else if (ColumnCaption == 'delete') {
      this.delete_record(event.data.companyid);
    }
    else if (ColumnCaption == 'add_new') {
      this.add_edit_record('0');
    }
  }




  change_cell_value(event: any) {
    this.table_info.primarykeyvalue = event.current_data[this.table_info.primarykeycolumn];
    this.table_info.fieldcolumn = event.column_config.ColumnName;
    this.table_info.fieldvalue = event.current_data[this.table_info.fieldcolumn];

  }



}
