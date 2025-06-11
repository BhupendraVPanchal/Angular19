import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProjectmemberComponent } from './projectmember.component';

describe('ProjectmemberComponent', () => {
  let component: ProjectmemberComponent;
  let fixture: ComponentFixture<ProjectmemberComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ProjectmemberComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ProjectmemberComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
