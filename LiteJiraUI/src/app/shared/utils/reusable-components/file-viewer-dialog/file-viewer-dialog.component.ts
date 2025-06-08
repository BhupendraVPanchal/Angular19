import { CommonModule } from '@angular/common';
import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';

@Component({
  selector: 'app-file-viewer-dialog',
  imports: [CommonModule],
  templateUrl: './file-viewer-dialog.component.html',
  styleUrl: './file-viewer-dialog.component.scss'
})
export class FileViewerDialogComponent {
  fileUrl: SafeUrl;
  fileType: string;

  constructor(
    public dialogRef: MatDialogRef<FileViewerDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { file: File, fileUrl: SafeUrl },
    private sanitizer: DomSanitizer
  ) {
    this.fileUrl = this.sanitizer.bypassSecurityTrustUrl(URL.createObjectURL(data.file));
    this.fileType = data.file.type;
  }

  close(): void {
    this.dialogRef.close();
  }
}
