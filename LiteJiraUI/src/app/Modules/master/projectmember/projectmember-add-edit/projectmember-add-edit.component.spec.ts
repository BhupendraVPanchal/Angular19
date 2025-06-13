import {ComponentFixture,TestBed}  from '@angular/core/testing';


import { ProjectmemberAddEditComponent }  from './projectmember-add-edit.component';


describe('ProjectmemberAddEditComponent', () => {
let component: ProjectmemberAddEditComponent;
let fixture: ComponentFixture<ProjectmemberAddEditComponent>;


beforeEach(() => {
TestBed.configureTestingModule({
declarations: [ProjectmemberAddEditComponent]
});
fixture = TestBed.createComponent(ProjectmemberAddEditComponent);
component = fixture.componentInstance;
fixture.detectChanges();
});


it('should create', () => {
expect(component).toBeTruthy();
});
});
