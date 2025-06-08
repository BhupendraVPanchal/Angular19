import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LiteCheckboxComponent } from './lite-checkbox.component';

describe('LiteCheckboxComponent', () => {
  let component: LiteCheckboxComponent;
  let fixture: ComponentFixture<LiteCheckboxComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LiteCheckboxComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LiteCheckboxComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
