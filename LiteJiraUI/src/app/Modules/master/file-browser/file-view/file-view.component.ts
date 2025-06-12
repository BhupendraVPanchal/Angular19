import { Component, CUSTOM_ELEMENTS_SCHEMA, Input, NO_ERRORS_SCHEMA, OnInit } from '@angular/core';
import { CommonService } from '../../../../shared/services/common.service';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';

@Component({
  selector: 'file-view',
  standalone: false,
  templateUrl: './file-view.component.html',
  styleUrls: ['./file-view.component.css']
  
})
export class FileViewComponent implements OnInit {
  @Input() fileUrl :any
  @Input()  fileType: string = '';
  @Input()  textContent: string = '';

  constructor(private service: CommonService, ) { }
  ngOnInit(): void {
    console.log(this.fileType);
    console.log(this.fileUrl);
    console.log(this.textContent);
    }

 
}
