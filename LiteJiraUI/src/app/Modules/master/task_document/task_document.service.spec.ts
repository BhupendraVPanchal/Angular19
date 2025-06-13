import { TestBed }  from '@angular/core/testing';
import { task_documentService } from './task_document.service';


describe('task_documentService', () => {
  let service: task_documentService;


beforeEach(() => {
TestBed.configureTestingModule({ });
service = TestBed.inject(task_documentService);
});


it('should be created', () => {
expect(service).toBeTruthy();
});
});
