import { TestBed } from '@angular/core/testing';
import { projectmasterService } from './projectmaster.service';


describe('projectmasterService', () => {
  let service: projectmasterService;


  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(projectmasterService);
  });


  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
