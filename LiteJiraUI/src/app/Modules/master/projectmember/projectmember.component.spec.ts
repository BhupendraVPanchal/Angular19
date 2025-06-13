import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ProjectmemberComponent } from './projectmember.component';




describe('ProjectmemberComponent', () => {
  let component: ProjectmemberComponent;
  let fixture: ComponentFixture<ProjectmemberComponent>;


  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ProjectmemberComponent]
    });
    fixture = TestBed.createComponent(ProjectmemberComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });


  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
