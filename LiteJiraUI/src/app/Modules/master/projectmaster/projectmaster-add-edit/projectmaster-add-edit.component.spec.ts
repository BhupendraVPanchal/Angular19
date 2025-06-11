import { ComponentFixture, TestBed } from '@angular/core/testing';


import { ProjectmasterAddEditComponent } from './projectmaster-add-edit.component';


describe('ProjectmasterAddEditComponent', () => {
  let component: ProjectmasterAddEditComponent;
  let fixture: ComponentFixture<ProjectmasterAddEditComponent>;


  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ProjectmasterAddEditComponent]
    });
    fixture = TestBed.createComponent(ProjectmasterAddEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });


  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
