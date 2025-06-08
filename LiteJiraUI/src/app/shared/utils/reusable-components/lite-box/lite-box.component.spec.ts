import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LiteBoxComponent } from './lite-box.component';

describe('LiteBoxComponent', () => {
  let component: LiteBoxComponent;
  let fixture: ComponentFixture<LiteBoxComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LiteBoxComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LiteBoxComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
