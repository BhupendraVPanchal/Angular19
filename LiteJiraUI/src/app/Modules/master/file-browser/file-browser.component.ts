import { Component, CUSTOM_ELEMENTS_SCHEMA, NO_ERRORS_SCHEMA, OnInit } from '@angular/core';
import { CommonService } from '../../../shared/services/common.service';
import { AppSettings } from '../../../appSettings';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-file-browser',
  standalone: false,
  templateUrl: './file-browser.component.html',
  styleUrls: ['./file-browser.component.css']
})
export class FileBrowserComponent implements OnInit {
  structure: any;
  fileViewUrl: any;
  extraFolderPath: string = '';

  constructor(private fbService: CommonService, private app_settings: AppSettings) { this.fileViewUrl = app_settings.fileViewUrl; }

  ngOnInit(): void {
    if (this.extraFolderPath == undefined || this.extraFolderPath == null || this.extraFolderPath == '') {
      this.fbService.getDirectoryStructureget().subscribe(data => {
        this.structure = data;
        console.log("this.structure", this.structure);
      });
    } else {
      this.fbService.getDirectoryStructure({ extraFolderPath: this.extraFolderPath }).subscribe(data => {
        this.structure = data;
        console.log("this.structure", this.structure);
      });
    }
  }

}
