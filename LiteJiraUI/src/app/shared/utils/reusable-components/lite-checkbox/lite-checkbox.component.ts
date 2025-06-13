import { Component, Input } from '@angular/core';
import { MaterialModule } from '../../material/material.module';
import { CommonModule } from '@angular/common';
import { FormGroup } from '@angular/forms';

@Component({
  selector: 'lite-checkbox',
  standalone: true,
  imports: [MaterialModule, CommonModule],
  templateUrl: './lite-checkbox.component.html',
  styleUrl: './lite-checkbox.component.scss'
})
export class LiteCheckboxComponent {
  @Input() formGroup: FormGroup;
  @Input() field: string = '';
  @Input() Caption: string;

  constructor() { }

  ngOnInit(): void {
  }
}
