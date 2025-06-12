import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CompanyComponent } from './company/company.component';
import { ProjectmasterComponent } from './projectmaster/projectmaster.component';
import { FileBrowserComponent } from './file-browser/file-browser.component';


const routes: Routes = [
  { path: 'company', component: CompanyComponent },
  { path: 'pm', component: ProjectmasterComponent },
  { path: 'fl', component: FileBrowserComponent }
  

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MasterRoutingModule { }
