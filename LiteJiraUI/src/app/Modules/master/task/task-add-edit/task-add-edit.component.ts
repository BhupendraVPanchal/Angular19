import { isPlatformBrowser } from '@angular/common';
import { Component, Inject, PLATFORM_ID, OnInit, OnDestroy } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { CommonService } from '../../../../shared/services/common.service';
import { CustomizerSettingsService } from '../../../../shared/customizer-settings/customizer-settings.service';
import { debounceTime, distinctUntilChanged, filter, switchMap } from 'rxjs';
import { ComponyService } from '../../../company/compony.service';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MasterService } from '../../master.service';
import { NotificationService } from '../../../../shared/services/notification.service';
import { LoaderService } from '../../../../shared/services/loader.service';
import { DynamicAlertService } from '../../../../shared/common/dynamic-alert/dynamic-alert.service';
import { taskService } from '../task.service';

@Component({
  selector: 'app-task-add-edit',
  standalone: false,
  templateUrl: './task-add-edit.component.html',
  styleUrl: './task-add-edit.component.scss'
})
export class TaskAddEditComponent implements OnInit {
  public entity_formgroup: FormGroup;
  public entity_formgroup_backup: FormGroup;
  public entity_code: number = 0;
  public file_server_path: string;

  public form_title: string = "task";
  public errormessage: string = "";
  public isnew: boolean = true;

  public project_data: any[] = [];
  public tasktype_data: any[] = [];
  public priority_data: any[] = [];
  public tagmaster_data: any[] = [];

  classApplied = true;


  public upload_file_src: string = "";
  public upload_file: File = null;  
  constructor(
    private fb: FormBuilder,
    @Inject(MAT_DIALOG_DATA) public data: any,
    public dialogRef: MatDialogRef<TaskAddEditComponent>,
    @Inject(PLATFORM_ID) private platformId: Object,
    public themeService: CustomizerSettingsService,
    private _notificationservice: NotificationService,
    private _dynamicalertservice: DynamicAlertService,
    private _loader: LoaderService,
    private _commonservice: CommonService,
    private _masterserviceobj: taskService
  ) { }

  ngOnInit(): void {
    if (isPlatformBrowser(this.platformId)) {
      this.init();

    }
  }

  init(): void {
    console.log(this.data);
    this.entity_code = this.data.entity_code > 0 ? this.data.entity_code : null;
    this.isnew = (this.entity_code == null);
    this.build_form(null);
    this.load_utility_data();
    this.fetch_data();
  }

  build_form(rowdata: any) {
    var obj: any = {}
      ;
    this.entity_formgroup = this.fb.group(
      {
        taskid: [rowdata != null ? rowdata.taskid : 0]
        , projectid: [rowdata != null ? rowdata.projectid : null]
        , refno: [rowdata != null ? rowdata.refno : null]
        , tasktitle: [rowdata != null ? rowdata.tasktitle : null]
        , taskdesc: [rowdata != null ? rowdata.taskdesc : null]
        , taskattachment: [rowdata != null ? rowdata.taskattachment : null]
        , priority: [rowdata != null ? rowdata.priority : null]
        , importance: [rowdata != null ? rowdata.importance : null]
        , tasktype: [rowdata != null ? rowdata.tasktype : null]
        //, taskdatetime: [rowdata != null ? rowdata.taskdatetime : null]
        //, startdate: [rowdata != null ? rowdata.startdate : null]
        //, duedate: [rowdata != null ? rowdata.duedate : null]
        //, task_status: [rowdata != null ? rowdata.task_status : null]
        //, assigneddate: [rowdata != null ? rowdata.assigneddate : null]
        //, isassigned: [rowdata != null ? rowdata.isassigned : null]
        //, iscancelled: [rowdata != null ? rowdata.iscancelled : null]
        //, cancelledby: [rowdata != null ? rowdata.cancelledby : null]
        //, cancelledon: [rowdata != null ? rowdata.cancelledon : null]
        //, commitmentdate: [rowdata != null ? rowdata.commitmentdate : null]
        //, insertsessionid: [rowdata != null ? rowdata.insertsessionid : null]
        //, updatesessionid: [rowdata != null ? rowdata.updatesessionid : null]
      }
    );
    this.entity_formgroup_backup = this._commonservice.cloneForm(this.entity_formgroup);

  }

  public load_utility_data(): void {

    this._masterserviceobj.get_projectmaster_help({})
      .subscribe(RtnData => {
        this.project_data = JSON.parse(JSON.stringify(RtnData.data));
      }, err => {

      });

    this._masterserviceobj.get_tasktype_help({})
      .subscribe(RtnData => {
        this.tasktype_data = JSON.parse(JSON.stringify(RtnData.data));
      }, err => {

      });


    this._masterserviceobj.get_prioritymaster_help({})
      .subscribe(RtnData => {
        this.priority_data = JSON.parse(JSON.stringify(RtnData.data));
      }, err => {

      });

    this._masterserviceobj.get_tagmaster_help({})
      .subscribe(RtnData => {
        this.tagmaster_data = JSON.parse(JSON.stringify(RtnData.data));
      }, err => {

      });


  }

  fetch_data() {
    if (this.entity_code > 0) {
      this._masterserviceobj.get_task({ taskid: this.entity_code })
        .subscribe({
          next: (RtnData) => {
            var datarow = RtnData.data[0];
            this.upload_file_src = RtnData?.file_path + '/' + datarow?.taskid + '/' + datarow?.taskattachment;
            this.build_form(datarow);
          }, error: (err_response) => {
            this._notificationservice.showToast(err_response['error']['message']);
            this.close_dialog(null);
          }
        });
    }
  }

  close_dialog(obj: any): void {
    this.dialogRef.close(obj);
    this.classApplied = !this.classApplied;
  }



  ngOnDestroy(): void {
  }

  save_entity_data(): void {
    this._loader.show();
    let snd_data = this.entity_formgroup.getRawValue();
    console.log("snd_data", snd_data);
    this._masterserviceobj.add_update_task(snd_data)
      .subscribe({
        next: (data) => {
          if (data.data[0].update_count > 0) {
            this._notificationservice.showToast('record updated successfully!');
            //this.toastr.success('record updated successfully!'); 
          }
          this.entity_code = data.data[0].taskid;
          snd_data.taskid = this.entity_code;
          this.upload_file_to_ftp(this.entity_code);
          this.close_dialog(null);
          this._loader.hide();
        }, error: (err_response) => {
          this._notificationservice.showToast(err_response['error']['message']);
        }
      });
  }

  custom_validators(): string {
    let err_message: string = '';
    if (err_message) {
      err_message = err_message.substring(0, err_message.length - 1);
    }
    return err_message;
  }

  formsubmit() {
    let err = this.custom_validators();
    if (err) {
      this._dynamicalertservice.fire({
        title: 'Error',
        text: err,
        icon: 'error',
        confirmButtonText: 'ok',
      })
      return;
    }
    if (this.entity_formgroup.valid) {
      if (this.entity_code) {
        this._dynamicalertservice.fire({
          title: 'Are you sure you want to update the data ?',
          text: 'This process is irreversible. ',
          icon: 'warning',
          showCancelButton: true,
          confirmButtonText: 'yes',
          cancelButtonText: 'no'
        }).then((r) => {
          if (r.isConfirmed) {
            this.save_entity_data();
          }
        })
      }
      else {
        this.save_entity_data();
      }
    }
    else {
      this._dynamicalertservice.fire({
        title: 'Error',
        text: 'validation error',
        icon: 'error',
        confirmButtonText: 'ok',
      })
      return;
    }
  }

  public delete_file_from_uploader(flag: any) {
    this.data.payload["logo"] = null;
  }
  public get_file_from_uploader(event: any): void {
    this.upload_file = event;
  }

  public upload_file_to_ftp(code: number) {
    const formData: FormData = new FormData();
    formData.append('Files', this.upload_file);
    this._commonservice.upload_files_to_directory(formData, "task/" + String(code)).subscribe();
  }


} 
