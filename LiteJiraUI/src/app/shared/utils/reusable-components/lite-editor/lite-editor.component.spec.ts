import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LiteEditorComponent } from './lite-editor.component';

describe('LiteEditorComponent', () => {
  let component: LiteEditorComponent;
  let fixture: ComponentFixture<LiteEditorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LiteEditorComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LiteEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
