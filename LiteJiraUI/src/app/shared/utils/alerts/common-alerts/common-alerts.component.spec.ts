import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CommonAlertsComponent } from './common-alerts.component';

describe('CommonAlertsComponent', () => {
  let component: CommonAlertsComponent;
  let fixture: ComponentFixture<CommonAlertsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CommonAlertsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CommonAlertsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
