import { TestBed }  from '@angular/core/testing';
import { taskService } from './task.service';


describe('taskService', () => {
  let service: taskService;


beforeEach(() => {
TestBed.configureTestingModule({ });
service = TestBed.inject(taskService);
});


it('should be created', () => {
expect(service).toBeTruthy();
});
});
