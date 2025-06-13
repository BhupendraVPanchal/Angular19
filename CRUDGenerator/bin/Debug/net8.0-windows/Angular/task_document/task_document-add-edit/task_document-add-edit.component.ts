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
 
@Component({ 
selector: 'app-task_document-add-edit', 
standalone: false, 
templateUrl: './task_document-add-edit.component.html', 
styleUrl: './task_document-add-edit.component.scss' 
}) 
export class Task_DocumentAddEditComponent implements OnInit 
{ 
public entity_formgroup: FormGroup; 
public entity_formgroup_backup: FormGroup; 
public entity_code: number = 0; 
public file_server_path: string; 
 
public form_title: string = "task_document"; 
public errormessage: string = ""; 
public isnew: boolean = true; 
 
classApplied = true; 
constructor( 
private fb: FormBuilder, 
@Inject(MAT_DIALOG_DATA) public data: any, 
public dialogRef: MatDialogRef<Task_DocumentAddEditComponent>, 
@Inject(PLATFORM_ID) private platformId: Object, 
public themeService: CustomizerSettingsService, 
private _notificationservice: NotificationService, 
private _dynamicalertservice: DynamicAlertService, 
private _loader: LoaderService, 
private _commonservice: CommonService, 
private _masterserviceobj: MasterService 
) { } 
 
ngOnInit() : void { 
if (isPlatformBrowser(this.platformId)) { 
this.init(); 
 
} 
} 
 
init() : void { 
console.log(this.data); 
this.entity_code = this.data.entity_code > 0 ? this.data.entity_code : null; 
this.isnew = (this.entity_code == null); 
this.build_form(null); 
this.fetch_data(); 
} 
 
build_form(rowdata: any) 
{ 
var obj: any = { } 
; 
this.entity_formgroup = this.fb.group( 
{ 
task_documentid: [rowdata != null ? rowdata.task_documentid : 0]
,taskid: [rowdata != null ? rowdata.taskid : null]
,document_name: [rowdata != null ? rowdata.document_name : null]
,document_description: [rowdata != null ? rowdata.document_description : null]
} 
); 
this.entity_formgroup_backup = this._commonservice.cloneForm(this.entity_formgroup); 
 
} 
 
fetch_data() 
{ 
if (this.entity_code > 0) 
{ 
this._masterserviceobj.get_task_document({ task_documentid: this.entity_code }) 
.subscribe({ 
next: (RtnData) => { 
var datarow = RtnData?.data[0]; 
this.build_form(datarow); 
}, error: (err_response) => { 
this._notificationservice.showToast(err_response['error']['message']); 
this.close_dialog(null); 
} 
}); 
} 
} 
 
close_dialog(obj: any) : void { 
this.dialogRef.close(obj); 
this.classApplied = !this.classApplied; 
} 
 
 
 
ngOnDestroy(): void { 
} 
 
save_entity_data(): void { 
this._loader.show(); 
let snd_data = this.entity_formgroup.getRawValue(); 
this._masterserviceobj.add_update_task_document(snd_data) 
.subscribe({ 
next: (data) => { 
if (data.data[0].update_count > 0) 
{ 
this._notificationservice.showToast('record updated successfully!'); 
//this.toastr.success('record updated successfully!'); 
} 
this.entity_code = data.data[0].task_documentid; 
snd_data.task_documentid = this.entity_code; 
this.close_dialog(null); 
this._loader.hide(); 
}, error: (err_response) => { 
this._notificationservice.showToast(err_response['error']['message']); 
} 
}); 
} 
 
custom_validators(): string { 
let err_message: string = ''; 
if (err_message) 
{ 
err_message = err_message.substring(0, err_message.length - 1); 
} 
return err_message; 
} 
 
formsubmit() { 
let err = this.custom_validators(); 
if (err) 
{ 
this._dynamicalertservice.fire({ 
title: 'Error', 
text: err, 
icon: 'error', 
confirmButtonText: 'ok', 
}) 
return; 
} 
if (this.entity_formgroup.valid) 
{ 
if (this.entity_code) 
{ 
this._dynamicalertservice.fire({ 
title: 'Are you sure you want to update the data ?', 
text: 'This process is irreversible. ', 
icon: 'warning', 
showCancelButton: true, 
confirmButtonText: 'yes', 
cancelButtonText: 'no' 
}).then((r) => { 
if (r.isConfirmed) 
{ 
this.save_entity_data(); 
} 
}) 
} 
else 
{ 
this.save_entity_data(); 
} 
} 
else 
{ 
this._dynamicalertservice.fire({ 
title: 'Error', 
text: 'validation error', 
icon: 'error', 
confirmButtonText: 'ok', 
}) 
return; 
} 
} 
 
 
} 
