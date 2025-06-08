import { Routes } from '@angular/router';
import { MainLayoutComponent } from './shared/Layouts/main-layout/main-layout.component';
import { AuthGuard } from './auth.guard';
import { ProjectManagementComponent } from './Modules/dashboard/project-management/project-management.component';
import { HelpDeskComponent } from './Modules/dashboard/help-desk/help-desk.component';
import { PmKanbanBoardComponent } from './Modules/prm/pm-kanban-board/pm-kanban-board.component';
import { KanbanBoardComponent } from './Modules/prm/kanban-board/kanban-board.component';
import { AllProjectsComponent } from './Modules/prm/all-projects/all-projects.component';
import { ToDoListComponent } from './Modules/prm/to-do-list/to-do-list.component';

export const routes: Routes = [
  
  {
    path: 'auth',
    loadChildren: () => import('./Modules/authantication/authantication.module').then(m => m.AuthanticationModule)
  },
  {
    path: 'authentication',
    loadChildren: () => import('./Modules/authantication/authantication.module').then(m => m.AuthanticationModule)
  },
  {
    path: '',
    component: MainLayoutComponent,
    canActivate: [AuthGuard],
    children: [
      {
        path: 'company',
        loadChildren: () => import('./Modules/company/company.module').then(m => m.CompanyModule)
      },
      {
        path: 'master',
        loadChildren: () => import('./Modules/master/master.module').then(m => m.MasterModule)
      },
      {
        path: '',
        redirectTo: 'dashboard',
        pathMatch: 'full'
      },
      { path: 'project-management', component: ProjectManagementComponent },
      { path: 'help-desk', component: HelpDeskComponent },
      { path: 'help-desk', component: HelpDeskComponent },
      { path: 'kanban-board', component: KanbanBoardComponent },
      { path: 'projects-list', component: AllProjectsComponent },
      { path: 'alltask', component: ToDoListComponent },

    ]
  },

  { path: '**', redirectTo: 'auth/login' }

  
];
