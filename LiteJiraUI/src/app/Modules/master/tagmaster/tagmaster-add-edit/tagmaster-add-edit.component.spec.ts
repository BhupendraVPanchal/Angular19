import { ComponentFixture, TestBed } from '@angular/core/testing';


import { TagmasterAddEditComponent } from './tagmaster-add-edit.component';


describe('TagmasterAddEditComponent', () => {
  let component: TagmasterAddEditComponent;
  let fixture: ComponentFixture<TagmasterAddEditComponent>;


  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [TagmasterAddEditComponent]
    });
    fixture = TestBed.createComponent(TagmasterAddEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });


  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
