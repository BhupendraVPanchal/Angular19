import {ComponentFixture,TestBed}  from '@angular/core/testing';


import { Task_DocumentAddEditComponent }  from './task_document-add-edit.component';


describe('Task_DocumentAddEditComponent', () => {
let component: Task_DocumentAddEditComponent;
let fixture: ComponentFixture<Task_DocumentAddEditComponent>;


beforeEach(() => {
TestBed.configureTestingModule({
declarations: [Task_DocumentAddEditComponent]
});
fixture = TestBed.createComponent(Task_DocumentAddEditComponent);
component = fixture.componentInstance;
fixture.detectChanges();
});


it('should create', () => {
expect(component).toBeTruthy();
});
});
