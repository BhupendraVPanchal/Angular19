import { Component, Input } from '@angular/core';
import { MaterialModule } from '../../material/material.module';
import { CommonModule } from '@angular/common';
import { FormGroup } from '@angular/forms';

@Component({
  selector: 'lite-switch',
  standalone: true,
  imports: [MaterialModule, CommonModule],
  templateUrl: './lite-switch.component.html',
  styleUrl: './lite-switch.component.scss'
})
export class LiteSwitchComponent {

  @Input() formGroup!: FormGroup;
  @Input() field: string = '';
  @Input() Caption!: string;


  constructor() { }

  ngOnInit(): void {
  }
}
