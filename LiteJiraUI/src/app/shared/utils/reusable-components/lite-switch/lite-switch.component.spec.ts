import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LiteSwitchComponent } from './lite-switch.component';

describe('LiteSwitchComponent', () => {
  let component: LiteSwitchComponent;
  let fixture: ComponentFixture<LiteSwitchComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LiteSwitchComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LiteSwitchComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
