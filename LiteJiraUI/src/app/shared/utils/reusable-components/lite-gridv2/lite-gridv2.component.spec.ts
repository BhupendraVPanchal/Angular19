import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LiteGridv2Component } from './lite-gridv2.component';

describe('LiteGridv2Component', () => {
  let component: LiteGridv2Component;
  let fixture: ComponentFixture<LiteGridv2Component>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LiteGridv2Component]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LiteGridv2Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
