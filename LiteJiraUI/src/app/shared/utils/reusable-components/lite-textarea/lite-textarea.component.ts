import { Component, Input, Output, EventEmitter, OnInit, AfterViewInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormGroup, AbstractControl } from '@angular/forms';
import { MaterialModule } from '../../material/material.module';

@Component({
  selector: 'lite-textarea',
  standalone: true,
  imports: [CommonModule, MaterialModule],
  templateUrl: './lite-textarea.component.html',
  styleUrls: ['./lite-textarea.component.scss']
})
export class LiteTextareaComponent implements OnInit, AfterViewInit {
  @Input() formGroup!: FormGroup;
  @Input() field!: string;
  @Input() Caption: string = '';
  @Input() IsReq: boolean = false;
  @Input() IsPattern: boolean = false;
  @Input() rows: number = 3;
  @Input() placeholder: string = '';
  @Input() pattern_msg: string = 'Invalid format';
  @Input() max_min_msg: string = 'Invalid length';
  @Input() maxLen: number = 500;
  @Output() valueChange = new EventEmitter<string>();

  constructor(private cdRef: ChangeDetectorRef) {}

  ngOnInit(): void {
    if (!this.formGroup) {
      throw new Error('FormGroup is required for LiteTextareaComponent');
    }
    if (!this.field) {
      throw new Error('Field name is required for LiteTextareaComponent');
    }
  }

  ngAfterViewInit() {
    this.cdRef.detectChanges();
  }

  setValue(value: string | null): void {
    const control = this.getFormControl();
    if (control) {
      control.setValue(value ?? '');
      control.markAsTouched();
      this.valueChange.emit(value ?? '');
    }
  }

  getFormControl(): AbstractControl | null {
    return this.formGroup.get(this.field);
  }

  autoGrow(textarea: HTMLTextAreaElement): void {
    textarea.style.height = 'auto'; // Reset height to recalculate
    textarea.style.height = `${textarea.scrollHeight}px`; // Set new height based on content
  }

  smartTextBlur(event: FocusEvent): void {
    console.log('Textarea lost focus:', event);
  }

  hasError(errorType: string): boolean {
    const control = this.getFormControl();
    return control ? control.hasError(errorType) && control.touched : false;
  }
}
