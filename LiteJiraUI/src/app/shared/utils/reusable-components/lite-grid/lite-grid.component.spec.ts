import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LiteGridComponent } from './lite-grid.component';

describe('LiteGridComponent', () => {
  let component: LiteGridComponent;
  let fixture: ComponentFixture<LiteGridComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LiteGridComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LiteGridComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

