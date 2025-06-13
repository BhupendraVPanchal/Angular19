import { ComponentFixture, TestBed } from '@angular/core/testing';
import { Task_DocumentComponent } from './task_document.component';




describe('Task_DocumentComponent', () => {
let component: Task_DocumentComponent;
let fixture: ComponentFixture<Task_DocumentComponent>;


beforeEach(() => {
TestBed.configureTestingModule({
declarations: [Task_DocumentComponent]
});
fixture = TestBed.createComponent(Task_DocumentComponent);
component = fixture.componentInstance;
fixture.detectChanges();
});


it('should create', () => {
expect(component).toBeTruthy();
});
});
