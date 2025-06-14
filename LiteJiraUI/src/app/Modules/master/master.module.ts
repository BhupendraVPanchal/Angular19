import { NgModule, CUSTOM_ELEMENTS_SCHEMA, NO_ERRORS_SCHEMA } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MasterRoutingModule } from './master-routing.module';
import { LiteBoxComponent } from '../../shared/utils/reusable-components/lite-box/lite-box.component';
import { LiteDdlComponent } from '../../shared/utils/reusable-components/lite-ddl/lite-ddl.component';
import { LiteEditorComponent } from '../../shared/utils/reusable-components/lite-editor/lite-editor.component';
import { LiteFileUploaderComponent } from '../../shared/utils/reusable-components/lite-file-uploader/lite-file-uploader.component';
import { LiteDatePickerComponent } from '../../shared/utils/reusable-components/lite-date-picker/lite-date-picker.component';
import { LiteTextareaComponent } from '../../shared/utils/reusable-components/lite-textarea/lite-textarea.component';
import { MaterialModule } from '../../shared/utils/material/material.module';
import { FileUploadModule } from '@iplab/ngx-file-upload';
import { LiteCheckboxComponent } from '../../shared/utils/reusable-components/lite-checkbox/lite-checkbox.component';
import { LiteSwitchComponent } from '../../shared/utils/reusable-components/lite-switch/lite-switch.component';
import { LiteGridComponent } from '../../shared/utils/reusable-components/lite-grid/lite-grid.component';
import { CompanyComponent } from './company/company.component';
import { CompanyAddEditComponent } from './company/company-add-edit/company-add-edit.component';
import { M2GridDivComponent } from '../../shared/utils/reusable-components/m2-grid-div/m2-grid-div.component';
import { MasterService } from './master.service';
import { ProjectmasterComponent } from './projectmaster/projectmaster.component';
import { ProjectmasterAddEditComponent } from './projectmaster/projectmaster-add-edit/projectmaster-add-edit.component';
import { FileBrowserComponent } from './file-browser/file-browser.component';
import { DirectoryComponent } from './file-browser/directory/directory.component';
import { FileViewComponent } from './file-browser/file-view/file-view.component';
import { LiteFileUploader2Component } from '../../shared/utils/reusable-components/lite-file-uploader2/lite-file-uploader2.component';
import { ProjectmemberComponent } from './projectmember/projectmember.component';
import { ProjectmemberAddEditComponent } from './projectmember/projectmember-add-edit/projectmember-add-edit.component';
import { TagmasterComponent } from './tagmaster/tagmaster.component';
import { TagmasterAddEditComponent } from './tagmaster/tagmaster-add-edit/tagmaster-add-edit.component';
import { TaskComponent } from './task/task.component';
import { TaskAddEditComponent } from './task/task-add-edit/task-add-edit.component';


@NgModule({
  declarations: [CompanyComponent, CompanyAddEditComponent
    , ProjectmasterComponent, ProjectmasterAddEditComponent
    , FileBrowserComponent, DirectoryComponent, FileViewComponent
    , ProjectmemberComponent, ProjectmemberAddEditComponent
    , TagmasterComponent, TagmasterAddEditComponent
    , TaskComponent, TaskAddEditComponent
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA, NO_ERRORS_SCHEMA],
  imports: [
    CommonModule,
    MasterRoutingModule,
    LiteBoxComponent,
    LiteDdlComponent,
    LiteEditorComponent,
    LiteFileUploaderComponent,
    LiteFileUploader2Component,
    LiteDatePickerComponent,
    LiteTextareaComponent,
    LiteDdlComponent,
    MaterialModule,
    FileUploadModule,
    LiteCheckboxComponent,
    LiteSwitchComponent,
    LiteGridComponent,
    M2GridDivComponent,
    LiteGridComponent
  ],
  exports: [CompanyComponent, CompanyAddEditComponent],
  providers: [MasterService]
})
export class MasterModule { }
