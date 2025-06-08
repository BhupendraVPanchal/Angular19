import { Component } from '@angular/core';
import { MaterialModule } from '../../material/material.module';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'lite-checkbox',
  standalone: true,
  imports: [MaterialModule, CommonModule],
  templateUrl: './lite-checkbox.component.html',
  styleUrl: './lite-checkbox.component.scss'
})
export class LiteCheckboxComponent {

}
