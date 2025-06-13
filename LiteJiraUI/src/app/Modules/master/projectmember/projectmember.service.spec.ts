import { TestBed } from '@angular/core/testing';
import { projectmemberService } from './projectmember.service';


describe('projectmemberService', () => {
  let service: projectmemberService;


  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(projectmemberService);
  });


  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
