import { Component, Inject, OnDestroy, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { dynamic_alert_input, dynamic_alert_output, dismiss_reason } from '../dynamic-alert/dynamic-alert.service';
import { Subscription } from 'rxjs';
import { MaterialModule } from '../../utils/material/material.module';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'm2-alert',
  imports: [MaterialModule, CommonModule],
  templateUrl: './dynamic-alert.component.html',
  styleUrls: ['./dynamic-alert.component.scss']
})
export class DynamicAlertComponent implements OnInit, OnDestroy {

  backdrop_subscription: Subscription;
  remaining_timer_time: number;
  remaining_timer_time_in_percent: number = 100;
  icon_class: string;
  icon_color: string;
  user_input: string = '';  // New property to store user input

  constructor(public dialog_ref: MatDialogRef<DynamicAlertComponent, dynamic_alert_output>,
    @Inject(MAT_DIALOG_DATA) public data: dynamic_alert_input) {
  }

  ngOnInit(): void {
    if (this.data.showConfirmButton == null) {
      this.data.showConfirmButton = true;
    }
    if (this.data.confirmButtonText == null) {
      this.data.confirmButtonText = 'ok';
    }
    if (this.data.cancelButtonText == null) {
      this.data.cancelButtonText = 'cancel';
    }
    if (this.data.denyButtonText == null) {
      this.data.denyButtonText = 'no';
    }

    if (this.data.icon) {
      if (this.data.icon == "error") {
        this.icon_class = "bi bi-x-lg";
        this.icon_color = "red";
      } else if (this.data.icon == "info") {
        this.icon_class = "bi bi-info";
        this.icon_color = "#60bae3";
      } else if (this.data.icon == "question") {
        this.icon_class = "bi bi-question-lg";
        this.icon_color = "";
      } else if (this.data.icon == "success") {
        this.icon_class = "bi bi-check-lg";
        this.icon_color = "#367f0e";
      } else if (this.data.icon == "warning") {
        this.icon_class = "bi bi-exclamation-lg";
        this.icon_color = "#ffcc00";
      }
    }

    this.backdrop_subscription = this.dialog_ref.backdropClick().subscribe(() => {
      this.dialog_ref.close({ isConfirmed: false, isDenied: false, dismiss: dismiss_reason.backdrop, isDismissed: true, userInput: this.user_input });
    })

    if (this.data.timer) {
      this.remaining_timer_time = this.data.timer;
      var interval_time: number = 120;
      const interval = setInterval(() => {
        this.remaining_timer_time -= interval_time;
        this.remaining_timer_time_in_percent = ((this.remaining_timer_time / this.data.timer) * 100);
        if (this.remaining_timer_time_in_percent < 0) {
          clearInterval(interval);
          this.dialog_ref.close({ isConfirmed: false, isDenied: false, dismiss: dismiss_reason.timer_end, isDismissed: true, userInput: this.user_input });
        }
      }, interval_time)
    }
  }

  confirmed(): void {
    this.dialog_ref.close({ isConfirmed: true, isDenied: false, dismiss: null, isDismissed: false, userInput: this.user_input });
  }

  denied(): void {
    this.dialog_ref.close({ isConfirmed: false, isDenied: true, dismiss: null, isDismissed: false, userInput: this.user_input });
  }

  cancelled(): void {
    this.dialog_ref.close({ isConfirmed: false, isDenied: false, dismiss: dismiss_reason.cancel, isDismissed: true, userInput: this.user_input });
  }

  closed(): void {
    this.dialog_ref.close({ isConfirmed: false, isDenied: false, dismiss: dismiss_reason.close, isDismissed: true, userInput: this.user_input });
  }

  ngOnDestroy(): void {
    this.backdrop_subscription.unsubscribe();
  }

}
