import { Component, EventEmitter, Input, OnInit, Output, ViewChild, ElementRef } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';
import { MatDialog } from '@angular/material/dialog';
import { FileViewerDialogComponent } from '../file-viewer-dialog/file-viewer-dialog.component';
import { CommonModule } from '@angular/common';
import { MaterialModule } from '../../material/material.module';
import { AlertService } from '../../alerts/alert-service.service';
import { AlertType } from '../../alerts/alert-types';

@Component({
  selector: 'app-lite-file-uploader',
  imports:[MaterialModule,CommonModule],
  templateUrl: './lite-file-uploader.component.html',
  styleUrls: ['./lite-file-uploader.component.scss']
})
export class LiteFileUploaderComponent implements OnInit {
  @Input() formGroup!: FormGroup;
  @Input() field!: string;
  @Input() multiple: boolean = false;
  @Input() accept: string = '';
  @Input() maxSizeMB: number = 5; 
  @Output() fileChangeEvent: EventEmitter<File[]> = new EventEmitter<File[]>();
  uploadedFiles: File[] = [];
  filePreviews: { file: File; preview: SafeUrl }[] = [];
  errorMessage: string = '';
  @ViewChild('fileInput') fileInput!: ElementRef;
  @ViewChild('slider', { static: false }) slider!: ElementRef;
  constructor(private sanitizer: DomSanitizer,private alertService:AlertService, private dialog: MatDialog) {}

  scrollLeft(): void {
    this.slider.nativeElement.scrollBy({ left: -80, behavior: 'smooth' });
  }

  scrollRight(): void {
    this.slider.nativeElement.scrollBy({ left: 80, behavior: 'smooth' });
  }

  ngOnInit(): void {
    if (this.formGroup && this.field) {
      this.formGroup.controls[this.field].setValue([]);
    }
  }

  onFileSelected(event: any): void {
    const files = Array.from(event.target.files) as File[];
    this.handleFiles(files);
  }

  onDrop(event: DragEvent): void {
    event.preventDefault();
    const files = Array.from(event.dataTransfer?.files || []);
    this.handleFiles(files);
  }

  handleFiles(files: File[]): void {
    this.errorMessage = '';

    files.forEach(file => {
      if (this.accept && !file.type.match(this.accept)) {
        this.errorMessage = `Invalid file type: ${file.name}`;
        this.alertService.showAlert(this.errorMessage, AlertType.DANGER);
        return;
      }

      if (file.size > this.maxSizeMB * 1024 * 1024) {
        this.errorMessage = `File ${file.name} exceeds the maximum size of ${this.maxSizeMB}MB`;
        this.alertService.showAlert(this.errorMessage, AlertType.DANGER);
        return;
      }

      let preview: SafeUrl;
      if (file.type.startsWith('image/') || file.type.startsWith('video/')) {
        preview = this.sanitizer.bypassSecurityTrustUrl(URL.createObjectURL(file));
      } else {
        preview = 'assets/file-icon.png';
      }

      this.uploadedFiles.push(file);
      this.filePreviews.push({ file, preview });
    });

    this.formGroup.controls[this.field].setValue(this.uploadedFiles);
    this.fileChangeEvent.emit(this.uploadedFiles);
  }


  removeFile(index: number): void {
    this.uploadedFiles.splice(index, 1);
    this.filePreviews.splice(index, 1);
    this.formGroup.controls[this.field].setValue(this.uploadedFiles);
  }

  openFileViewer(file: File): void {
    const fileUrl = (file.type.startsWith('image/') || file.type.startsWith('video/'))
      ? this.sanitizer.bypassSecurityTrustUrl(URL.createObjectURL(file))
      : '';

    this.dialog.open(FileViewerDialogComponent, {
      data: { file, fileUrl },
      width: '80%',
      height: '80%'
    });
  }


  allowDrop(event: DragEvent): void {
    event.preventDefault();
  }

  triggerFileInput(): void {
    this.fileInput.nativeElement.click();
  }
}
