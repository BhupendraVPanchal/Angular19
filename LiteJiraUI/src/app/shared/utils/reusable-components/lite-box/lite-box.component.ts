import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import {  FormGroup } from '@angular/forms';
import { MaterialModule } from '../../material/material.module';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-lite-box',
  standalone:true,
  imports:[MaterialModule,CommonModule],
  templateUrl: './lite-box.component.html',
  styleUrls: ['./lite-box.component.scss']
})
export class LiteBoxComponent implements OnInit {
  
    @Input() formGroup!: FormGroup;
    @Input() field: string = '';
    @Input() datatype: string = 'text';

    @Input() IsReq: boolean = false;
    @Input() IsPattern: boolean = false;
    @Input() IsBlockAllChar: boolean = false;
    @Input() IsBlockAllNum: boolean = false; 
    @Input() pattern_msg: string = '';
    @Input() check_max_min: boolean = true;
    @Input() max_min_msg: string = '';
    @Input() Caption!: string;
    @Input() lable!: string;
    @Input() Isnum: string = 'text';
    @Output() TextValueChangeEvent: EventEmitter<any> = new EventEmitter<any>();
    @Output() TextBlurEvent: EventEmitter<any> = new EventEmitter<any>();
    @Input() maxLen: number = 500;
    @Input() minLen: number = 0;
    @Input() customclassname: string = '';
  
    @Input() block_spl_char_and_space: boolean = false;
    @Input() block_spl_char: boolean = false;
    @Input() block_space: boolean = false;
    @Input() is_OnlyAlphabet: boolean = false;
    @Input() is_bit: boolean = false;
    @Input() is_numeric: boolean = false;
    @Input() is_numeric_with_minus: boolean = false;
    @Input() is_numeric_without_minus_and_decimal: boolean = false;
    @Input() maxnumvalue?: string  ;
    @Input() digitafterdecimal?: string ;
    @Input() disabled: boolean = true;
    @Input() readonly: boolean = false;
    public hide :boolean = true;
    public errorMessages: { [key: string]: boolean } = {};
  
  
    ngOnInit(): void {
  
      //this.formGroup.controls[this.field].valueChanges.subscribe(() => {
      //  this.errorMessages = {
      //    required: this.hasError(this.field, 'required'),
      //    pattern: this.hasError(this.field, 'pattern')
      //  };
      //});
  
    }
  
  
  
  
    public hasError = (controlName: string, errorName: string) => {
      const control = this.formGroup?.controls[controlName];
      return control ? control.hasError(errorName) : false;
    }
  
  
    public IsValid = (controlName: string) => {
      return this.formGroup.controls[controlName].valid;
    }
  
    public smartTextValueChange(event: any): void {
      this.TextValueChangeEvent.emit(event);
    }
    public smartTextBlur(event: any): void {
      this.TextBlurEvent.emit(event);
      this.trimLeadingSpaces();
    }
  
  
    isNumberWithoutMinusAndDecimal(evt: any): boolean {
      evt = (evt) ? evt : window.event;
      let charCode = evt.charCode;
      if ((charCode >= 48 && charCode <= 57) || (charCode == 32)) {
        return true;
      }
      return false;
    }
  
    /* #region Key Press Events */
    isNumberKey(evt: any): boolean {
      evt = (evt) ? evt : window.event;
      let control_value: string = '';
      if (this.formGroup.controls[this.field].value != null) {
        control_value = this.formGroup.controls[this.field].value;
      }
      return /^\d*\.?\d*$/.test(control_value + evt.key);
  
  
    }
  
    isNumberOrMinusSignKey(evt: any): boolean {
      evt = (evt) ? evt : window.event;
      let control_value: string = '';
      if (this.formGroup.controls[this.field].value != null) {
        control_value = this.formGroup.controls[this.field].value;
      }
      return /^\-?\d*\.?\d*$/.test(control_value + evt.key);
    }
  
    bit_key(evt: any): boolean {
      evt = (evt) ? evt : window.event;
      let charCode = evt.charCode;
      let control_value: string = '';
      if (this.formGroup.controls[this.field].value != null) {
        control_value = this.formGroup.controls[this.field].value;
      }
      if ((charCode >= 48 && charCode <= 49 && (control_value == null || control_value.length <= 0)) || (charCode == 8)) {
        return true;
      }
      return false;
  
    }
  
    public trimLeadingSpaces(): void {
      const control = this.formGroup.get(this.field);
      if (control) {
        let trimmedValue = control?.value?.replace(/^\s+/, '');
        control.setValue(trimmedValue, { emitEvent: false });
      }
    }
    blockSpace(evt: any): boolean {
      evt = (evt) ? evt : window.event;
      var charCode = (evt.which) ? evt.which : evt.keyCode;
      return (charCode != 32);
  
    }
  
    blockSpecialChar(evt: any): boolean {
      evt = (evt) ? evt : window.event;
      var charCode = (evt.which) ? evt.which : evt.keyCode;
      return ((charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123) || (charCode == 32) || (charCode >= 48 && charCode <= 57));
  
    }
  
    alphabetOnly(evt: any): boolean {
      evt = (evt) ? evt : window.event;
      var charCode = (evt.which) ? evt.which : evt.keyCode;
      return ((charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123) || (charCode == 32));
      //var element: HTMLInputElement = (evt.target as HTMLInputElement);
      ////var previous_value = element.getAttribute("data_previous_value");
      //if (!(/^[A-Za-z ]+$/.test(element.value))) {
      //  element.value = this.old_value;
      //}
    }
  
    blockSpecialCharAndSpace(evt: any): boolean {
      evt = (evt) ? evt : window.event;
      var charCode = (evt.which) ? evt.which : evt.keyCode;
      return ((charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123) || (charCode == 8) || (charCode >= 48 && charCode <= 57));
  
    }
    
  
  text_key_event(event: any): boolean {
    switch (this.datatype.toLowerCase()) {
      case 'number':
        return this.isNumberKey(event);

      case 'onlychar':
        return this.alphabetOnly(event);

      case 'email':
        // Allow common email characters
        const charCode = event.which || event.keyCode;
        const allowedChars = /^[a-zA-Z0-9@._\-]+$/;
        return allowedChars.test(String.fromCharCode(charCode));

      default:
        // Fallback to flags if datatype not specified
        if (this.maxnumvalue || this.digitafterdecimal) {
          return true;
        }
        else if (this.block_spl_char_and_space) {
          return this.blockSpecialCharAndSpace(event);
        } else if (this.block_spl_char) {
          return this.blockSpecialChar(event);
        } else if (this.block_space) {
          return this.blockSpace(event);
        } else if (this.is_numeric) {
          return this.isNumberKey(event);
        } else if (this.is_numeric_with_minus) {
          return this.isNumberOrMinusSignKey(event);
        } else if (this.is_numeric_without_minus_and_decimal) {
          return this.isNumberWithoutMinusAndDecimal(event);
        } else if (this.is_bit) {
          return this.bit_key(event);
        } else if (this.is_OnlyAlphabet) {
          return this.alphabetOnly(event);
        } else {
          return true;
        }
    }
  }

  
    public handleKeydown(evt: KeyboardEvent): void {
      if (this.IsBlockAllChar) {
        this.blockAllChar(evt);
      }
      if (this.IsBlockAllNum) {
        this.blockAllNum(evt);
      }
    }
    public blockAllChar(evt: KeyboardEvent): void {
      const charCode = evt.which ? evt.which : evt.keyCode;
      const allowedKeys = [
        8, 9, 13, 16, 20, 17, 27, 32, 35, 36, 37, 38, 39, 40, 46
      ];
      const isCtrlOrCmd = evt.ctrlKey || evt.metaKey;
      const isCtrlCV = isCtrlOrCmd && (charCode === 67 || charCode === 86);
      const isShiftInsert = evt.shiftKey && charCode === 45;
      const isCtrlA = isCtrlOrCmd && charCode === 65;
      if (isCtrlCV || isShiftInsert || isCtrlA || allowedKeys.includes(charCode) ||
        (charCode >= 48 && charCode <= 57) ||
        (charCode >= 96 && charCode <= 105)) {
        return;
      }
      evt.preventDefault();
    }
  
  
    blockAllNum(evt: any): void {
      var mumcode = (evt.which) ? evt.which : evt.keyCode;
      if (mumcode === 48 || mumcode === 49 || mumcode === 50 || mumcode === 51 || mumcode === 52 || mumcode === 53 || mumcode === 54 || mumcode === 55 || mumcode === 56 || mumcode === 57 || mumcode === 96 || mumcode === 97 || mumcode === 98 || mumcode === 99 || mumcode === 100 || mumcode === 101 || mumcode === 102 || mumcode === 103 || mumcode === 104 || mumcode === 105) {
        evt.preventDefault();
      }
    }
  
}
