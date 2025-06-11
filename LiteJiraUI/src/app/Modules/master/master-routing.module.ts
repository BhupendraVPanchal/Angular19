import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CompanyComponent } from './company/company.component';
import { ProjectmasterComponent } from './projectmaster/projectmaster.component';

const routes: Routes = [
  { path: 'company', component: CompanyComponent },
  { path: 'projectmaster', component: ProjectmasterComponent },

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MasterRoutingModule { }
