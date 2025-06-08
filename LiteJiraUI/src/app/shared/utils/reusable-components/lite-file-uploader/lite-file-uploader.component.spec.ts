import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LiteFileUploaderComponent } from './lite-file-uploader.component';

describe('LiteFileUploaderComponent', () => {
  let component: LiteFileUploaderComponent;
  let fixture: ComponentFixture<LiteFileUploaderComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LiteFileUploaderComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LiteFileUploaderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
