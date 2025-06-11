import { Component } from '@angular/core';
import { ProjectsOverviewComponent } from './projects-overview/projects-overview.component';
import { AllProjectsComponent } from '../../prm/all-projects/all-projects.component';
import { ProjectsProgressComponent } from './projects-progress/projects-progress.component';
import { ToDoListComponent } from '../../prm/to-do-list/to-do-list.component';
import { TeamMembersComponent } from './team-members/team-members.component';

@Component({
    selector: 'app-project-management',
    imports: [ProjectsOverviewComponent, AllProjectsComponent, ProjectsProgressComponent, TeamMembersComponent],
    templateUrl: './project-management.component.html',
    styleUrl: './project-management.component.scss'
})
export class ProjectManagementComponent {}
