import { Component, Input, OnInit } from '@angular/core';
import { FormGroup, AbstractControl } from '@angular/forms';
import { MaterialModule } from '../../material/material.module';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'lite-date-picker',
  imports:[MaterialModule,CommonModule],
  templateUrl: './lite-date-picker.component.html',
  styleUrls: ['./lite-date-picker.component.scss']
})
export class LiteDatePickerComponent implements OnInit {
  @Input() formGroup!: FormGroup;
  @Input() field!: string;
  @Input() startField!: string;
  @Input() endField!: string;
  @Input() Caption: string = '';
  @Input() IsReq: boolean = false;
  @Input() selectionType: 'single' | 'range' = 'single';
  @Input() minDate: Date =new Date(2023, 0, 1);
  @Input() maxDate: Date=new Date(2025, 11, 31);

  constructor() {}

  ngOnInit(): void {
    if (!this.formGroup) {
      throw new Error('FormGroup is required for LiteDatePickerComponent');
    }
  }

  getFormControl(fieldName: string): AbstractControl | null {
    return this.formGroup.get(fieldName);
  }

  hasError(fieldName: string, errorType: string): boolean {
    const control = this.getFormControl(fieldName);
    return control ? control.hasError(errorType) && control.touched : false;
  }

  onDateChange(event: any, fieldName: string): void {
    const control = this.getFormControl(fieldName);
    if (control) {
      const selectedDate = new Date(event.value);
      if (this.minDate && selectedDate < this.minDate) {
        control.setErrors({ minDate: true });
      } else if (this.maxDate && selectedDate > this.maxDate) {
        control.setErrors({ maxDate: true });
      } else {
        control.setValue(selectedDate);
        control.markAsTouched();
      }
    }
    console.log(`Date changed for ${fieldName}:`, event.value);
  }
}