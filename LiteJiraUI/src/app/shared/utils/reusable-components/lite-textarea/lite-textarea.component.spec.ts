import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LiteTextareaComponent } from './lite-textarea.component';

describe('LiteTextareaComponent', () => {
  let component: LiteTextareaComponent;
  let fixture: ComponentFixture<LiteTextareaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LiteTextareaComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LiteTextareaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
