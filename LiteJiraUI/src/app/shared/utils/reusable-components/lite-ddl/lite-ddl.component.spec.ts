import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LiteDdlComponent } from './lite-ddl.component';

describe('LiteDdlComponent', () => {
  let component: LiteDdlComponent;
  let fixture: ComponentFixture<LiteDdlComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LiteDdlComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LiteDdlComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
