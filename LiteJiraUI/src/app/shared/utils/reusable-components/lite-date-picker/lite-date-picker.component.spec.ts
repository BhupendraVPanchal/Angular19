import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LiteDatePickerComponent } from './lite-date-picker.component';

describe('LiteDatePickerComponent', () => {
  let component: LiteDatePickerComponent;
  let fixture: ComponentFixture<LiteDatePickerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LiteDatePickerComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LiteDatePickerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
