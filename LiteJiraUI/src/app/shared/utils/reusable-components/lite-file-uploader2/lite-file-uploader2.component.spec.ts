import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LiteFileUploader2Component } from './lite-file-uploader2.component';

describe('LiteFileUploader2Component', () => {
  let component: LiteFileUploader2Component;
  let fixture: ComponentFixture<LiteFileUploader2Component>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LiteFileUploader2Component]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LiteFileUploader2Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
