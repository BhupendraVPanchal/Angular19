import { TestBed } from '@angular/core/testing';

import { ComponyService } from './compony.service';

describe('ComponyService', () => {
  let service: ComponyService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ComponyService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
