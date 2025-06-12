import { Component, Input, OnInit } from '@angular/core';
import { FormGroup, AbstractControl } from '@angular/forms';
import { MaterialModule } from '../../material/material.module';
import { CommonModule } from '@angular/common';
import { DateAdapter, MAT_DATE_FORMATS, MAT_DATE_LOCALE } from '@angular/material/core';


export const MY_DATE_FORMATS = {
  parse: {
    dateInput: 'DD/MM/YYYY',
  },
  display: {
    dateInput: 'DD/MM/YYYY',
    monthYearLabel: 'MMMM YYYY',
    dateA11yLabel: 'LL',
    monthYearA11yLabel: 'MMMM YYYY',
  }
};

@Component({
  selector: 'lite-date-picker',
  imports:[MaterialModule,CommonModule],
  templateUrl: './lite-date-picker.component.html',
  styleUrls: ['./lite-date-picker.component.scss'],
  providers: [
    { provide: MAT_DATE_FORMATS, useValue: MY_DATE_FORMATS }
  ]
})
export class LiteDatePickerComponent implements OnInit {
  @Input() formGroup!: FormGroup;
  @Input() field!: string;
  @Input() startField!: string;
  @Input() endField!: string;
  @Input() Caption: string = '';
  @Input() IsReq: boolean = false;
  @Input() selectionType: 'single' | 'range' = 'single';
  @Input() minDate: Date =new Date();
  @Input() maxDate: Date=new Date(2050, 12, 31);

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
      if (this.minDate && selectedDate <= this.minDate) {
        control.setErrors({ minDate: true });
      } else if (this.maxDate && selectedDate >= this.maxDate) {
        control.setErrors({ maxDate: true });
      } else {
        control.setValue(selectedDate);
        control.markAsTouched();
      }
    }
    console.log(`Date changed for ${fieldName}:`, event.value);
  }
}
