import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FileBrowserComponent } from './file-browser.component';

describe('FileBrowserComponent', () => {
  let component: FileBrowserComponent;
  let fixture: ComponentFixture<FileBrowserComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [FileBrowserComponent]
    });
    fixture = TestBed.createComponent(FileBrowserComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
