import { TestBed } from '@angular/core/testing';

import { DynamicAlertService } from './dynamic-alert.service';

describe('DynamicAlertService', () => {
  let service: DynamicAlertService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DynamicAlertService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
