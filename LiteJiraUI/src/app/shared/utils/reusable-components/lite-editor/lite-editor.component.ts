import { Component, Input, Output, EventEmitter, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, AbstractControl } from '@angular/forms';
import { Editor, NgxEditorModule, Toolbar } from 'ngx-editor';
import { CommonModule } from '@angular/common';
import { MaterialModule } from '../../material/material.module';

@Component({
  selector: 'lite-editor',
  standalone: true,
  imports: [CommonModule,NgxEditorModule,MaterialModule],
  templateUrl: './lite-editor.component.html',
  styleUrls: ['./lite-editor.component.scss']
})
export class LiteEditorComponent implements OnInit, OnDestroy {
  @Input() formGroup!: FormGroup;
  @Input() field!: string;
  @Input() Caption: string = '';
  @Input() IsReq: boolean = false;
  @Output() valueChange = new EventEmitter<string>();
  editor!: Editor;

  toolbar: Toolbar = [
    ['bold', 'italic'],
    ['underline', 'strike'],
    ['code', 'blockquote'],
    ['ordered_list', 'bullet_list'],
    [{ heading: ['h1', 'h2', 'h3', 'h4', 'h5', 'h6'] }],
    ['link', 'image'],
    ['text_color', 'background_color'],
    ['align_left', 'align_center', 'align_right', 'align_justify'],
  ];

  ngOnInit(): void {
    this.editor = new Editor();
    if (!this.formGroup) {
      throw new Error('FormGroup is required for LiteEditorComponent');
    }
    if (!this.field) {
      throw new Error('Field name is required for LiteEditorComponent');
    }
  }

  getFormControl(): AbstractControl | null {
    return this.formGroup.get(this.field);
  }

  setValue(value: string): void {
    const control = this.getFormControl();
    if (control) {
      control.setValue(value);
      control.markAsTouched();
      this.valueChange.emit(value);
    }
  }

  hasError(errorType: string): boolean {
    const control = this.getFormControl();
    return control ? control.hasError(errorType) && control.touched : false;
  }

  ngOnDestroy(): void {
    if (this.editor) {
      this.editor.destroy();
    }
  }
}
