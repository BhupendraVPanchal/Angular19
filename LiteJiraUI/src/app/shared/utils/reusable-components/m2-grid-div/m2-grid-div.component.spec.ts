import { ComponentFixture, TestBed } from '@angular/core/testing';

import { M2GridDivComponent } from './m2-grid-div.component';

describe('M2GridDivComponent', () => {
  let component: M2GridDivComponent;
  let fixture: ComponentFixture<M2GridDivComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [M2GridDivComponent]
    });
    fixture = TestBed.createComponent(M2GridDivComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
