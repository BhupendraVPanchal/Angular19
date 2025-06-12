import { Component, Input, OnInit, Output, EventEmitter, ViewChild, ElementRef } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { AlertService } from '../../alerts/alert-service.service';
import { AlertType } from '../../alerts/alert-types';
import { NotificationService } from '../../../../shared/services/notification.service';
import { MaterialModule } from '../../material/material.module';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-lite-file-uploader2',
  standalone: true,
  imports: [MaterialModule, CommonModule],
  templateUrl: './lite-file-uploader2.component.html',
  styleUrl: './lite-file-uploader2.component.scss'
})
export class LiteFileUploader2Component implements OnInit {

  @Input() formGroup: FormGroup;
  @Input() field: string = '';
  @Input() Caption: string;
  @Input() IsReq: boolean = false;
  @Input() accept: string = '';
  @Output() GetFileEvent: EventEmitter<any> = new EventEmitter<any>();
  @Output() DeleteFile: EventEmitter<any> = new EventEmitter<any>();
  @Input() public BannerViewURL: any;
  public FileObj: File = null;
  public AcceptArr: any[] = [];

  @ViewChild('fileInput') fileInput!: ElementRef;

  public fileType: any[] = ['image/jpg', 'image/jpeg', 'image/png', 'image/gif', 'image/bmp', 'application/pdf', 'image/svg+xml', 'video/mp4', 'video/webm'];
  public media_fileType: any[] = ['image/jpg', 'image/jpeg', 'image/png', 'image/gif', 'image/bmp', 'image/svg+xml', 'video/mp4', 'video/webm'];

  public fileInputValue: string = '';
  constructor(private errorMsgService: AlertService,private _notificationservice: NotificationService,) { }

  ngOnInit(): void {
    if (this.accept != '') {
    } else {
      this.accept = this.fileType.join(',');
    }
    this.fileInputValue = this.formGroup?.controls[this.field]?.value;
  }

  Delete() {
    this.BannerViewURL = "";
    this.formGroup.controls[this.field].setValue("");
    const fileInput = document?.getElementById(this.field) as HTMLInputElement;
    if (fileInput) {
      fileInput.value = ''; // Clear the value to reset the input
    }
    this.DeleteFile.emit();
  }

  handleFile(event: any) {
    if (event?.target?.files?.length > 0) {
      const file = event.target.files[0];
      this.AcceptArr = this.accept.split(',');

      if (this.AcceptArr.includes(file.type)) {
        this.FileObj = file;
        this.fileInputValue = this.FileObj.name;
        this.formGroup.controls[this.field].setValue(this.FileObj.name);

        const reader = new FileReader();
        reader.readAsDataURL(this.FileObj);
        reader.onload = (e) => {
          this.BannerViewURL = e.target.result;
        };

        this.GetFileEvent.emit(this.FileObj);
      } else {
        event.target.value = null;
        this._notificationservice.showToast("Invalid File Type");

        this.Delete();
      }
    }
  }


  triggerFile() {
    this.fileInput.nativeElement.click();
  }

  validateFileName(fileName: string): boolean {
    const validNamePattern = /^[a-zA-Z0-9_.-]+$/;
    const maxLength = 100; // Set max length for file name

    if (!validNamePattern.test(fileName)) {
      this._notificationservice.showToast('File name contains invalid characters. Only alphanumeric, underscores, hyphens, and dots are allowed.');
      return false;
    }

    if (fileName.length > maxLength) {
      this._notificationservice.showToast(
        `File name exceeds the maximum length of ${maxLength} characters.`
      );
      return false;
    }
    return true;
  }

  getFileTypeFromName(fileName: string): 'image' | 'video' | 'unknown' {
    if (!fileName) return 'unknown';
    const extension = fileName.split('.').pop()?.toLowerCase();
    if (['jpg', 'jpeg', 'png', 'gif', 'bmp', 'svg'].includes(extension)) {
      return 'image';
    } else if (['mp4', 'webm'].includes(extension)) {
      return 'video';
    } else {
      return 'unknown';
    }
  }

  allowDrop(event: DragEvent): void {
    event.preventDefault();
  }

  onDrop(event: DragEvent): void {
    event.preventDefault();
    const files = Array.from(event.dataTransfer?.files || []);
    this.handleFile(files);
  }


}
