import { Component, CUSTOM_ELEMENTS_SCHEMA, NO_ERRORS_SCHEMA, OnInit } from '@angular/core';
import { Input } from '@angular/core';
import { CommonService } from '../../../../shared/services/common.service';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';

@Component({
  selector: 'app-directory',
  standalone: false,
  templateUrl: './directory.component.html',
  styleUrls: ['./directory.component.css']
})
export class DirectoryComponent implements OnInit  {
  
  @Input() node: any;
  collapsed: boolean = true;
  searchTerm: string = '';
  fileUrl: any;
  fileType: string = '';
  textContent: string = '';

  constructor(private service: CommonService, private sanitizer: DomSanitizer) { }

  ngOnInit(): void {
    console.log(this.node)
  }
  isImage(fileName: string): boolean {
    return /\.(jpg|jpeg|png|gif|webp|bmp|svg)$/i.test(fileName);
  }
  toggleCollapse(): void {
    this.collapsed = !this.collapsed;
  }
  get filteredFiles(): any[] {
    return this.node.files.filter((file: any) =>
      file.name.toLowerCase().includes(this.searchTerm.toLowerCase())
    );
  }

  get filteredDirectories(): any[] {
    return this.node.directories.filter((dir: any) =>
      dir.name.toLowerCase().includes(this.searchTerm.toLowerCase())
    );
  }
  onFolderClick(dir:any) {

  }

  downlodFile(name: string) {
    const fileName = name.split('/').pop();
    let obj = {
      "filePaths": name
    }
    this.service.Download(obj).subscribe(blob => {
      const url = window.URL.createObjectURL(blob);
      const a = document.createElement('a');
      a.href = url;
      a.download = fileName;
      a.click();
      window.URL.revokeObjectURL(url);
    }, error => {
      console.error('Download failed:', error);
    });
  }
  previewFile(name: string, type: string) {
    alert(type)
    this.fileUrl = this.sanitizer.bypassSecurityTrustResourceUrl(name);
    this.fileType = type;
    this.textContent = name.split('/').pop();
  }
}
