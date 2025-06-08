import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { MatSelectChange } from '@angular/material/select';
import { CommonModule } from '@angular/common';
import { MaterialModule } from '../../material/material.module';

@Component({
  selector: 'lite-ddl',
  standalone: true,
  imports: [MaterialModule, CommonModule],
  templateUrl: './lite-ddl.component.html',
  styleUrls: ['./lite-ddl.component.scss']
})
export class LiteDdlComponent {
  @Input() formGroup!: FormGroup;
  @Input() field: string = '';
  @Input() options: any[] = [];
  @Input() displayField: string = 'display';
  @Input() valueField: string = 'value';
  @Input() isMultiSelect: boolean = false;
  @Input() Caption: string = 'Select an option';
  @Input() disabled: boolean = false;
  @Input() readonly: boolean = false;
  @Input() IsReq: boolean = false;
  @Output() selectionChange: EventEmitter<{ 
    previousValue: any, 
    currentValue: any, 
    selectedOptions: any[] 
  }> = new EventEmitter();

  previousValue: any = null;

  ngOnInit() {
    this.previousValue = this.formGroup.controls[this.field]?.value || null;
  }

  onSelectionChange(event: MatSelectChange) {
    const selectedValues = event.value;
    const selectedOptions = this.options.filter(opt => 
      this.isMultiSelect ? selectedValues.includes(opt[this.valueField]) 
                         : opt[this.valueField] === selectedValues
    );

    const eventPayload = {
      previousValue: this.previousValue,
      currentValue: selectedValues,
      selectedOptions: selectedOptions
    };

    this.previousValue = selectedValues;
    this.selectionChange.emit(eventPayload);
  }
}
