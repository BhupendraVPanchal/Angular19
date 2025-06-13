import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CompanyComponent } from './company/company.component';
import { ProjectmasterComponent } from './projectmaster/projectmaster.component';
import { FileBrowserComponent } from './file-browser/file-browser.component';
import { ProjectmemberComponent } from './projectmember/projectmember.component';
import { TagmasterComponent } from './tagmaster/tagmaster.component';
import { TaskComponent } from './task/task.component';


const routes: Routes = [
  { path: 'company', component: CompanyComponent },
  { path: 'project', component: ProjectmasterComponent },
  { path: 'members', component: ProjectmemberComponent },
  { path: 'tag', component: TagmasterComponent },
  { path: 'task', component: TaskComponent },
  { path: 'fl', component: FileBrowserComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MasterRoutingModule { }
