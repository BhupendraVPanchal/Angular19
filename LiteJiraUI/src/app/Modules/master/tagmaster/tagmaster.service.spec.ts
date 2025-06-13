import { TestBed }  from '@angular/core/testing';
import { tagmasterService } from './tagmaster.service';


describe('tagmasterService', () => {
  let service: tagmasterService;


beforeEach(() => {
TestBed.configureTestingModule({ });
service = TestBed.inject(tagmasterService);
});


it('should be created', () => {
expect(service).toBeTruthy();
});
});
