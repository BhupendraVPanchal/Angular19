import { Component, EventEmitter, OnInit } from '@angular/core';
import { AlertType ,ALERT_CLASS_MAP } from '../alert-types';
import { MaterialModule } from '../../material/material.module';
import { CommonModule } from '@angular/common';
import { AlertService } from '../alert-service.service';
@Component({
  selector: 'app-common-alerts',
  imports: [MaterialModule, CommonModule],
  templateUrl: './common-alerts.component.html',
  styleUrl: './common-alerts.component.scss'
})
export class CommonAlertsComponent {
  alert: { message: string; type: AlertType; timeout: number;showConfirm?: boolean } | null = null;

  constructor(private alertService: AlertService) {
    this.alertService.alert$.subscribe(alert => {
      if (alert) {
       alert.showConfirm??  this.showConfirmation() 
        this.alert = alert;
      } else {
        this.alert = null; 
      }
    });
  }
  showConfirmation() {
    if (this.alert) {
      this.alert.showConfirm = true;
      this.alert=null;
    }
  }

  confirmDismiss(): Promise<void> {
    return new Promise((resolve) => {
      this.alert = null;
      resolve();
    });
  }
  
  cancelDismiss(): Promise<void> {
    return new Promise((resolve) => {
      if (this.alert) {
        this.alert.showConfirm = false;
        this.alertService.clearAlert();
      }
      resolve();
    });
  }
  
  getAlertClass(type: AlertType): string {
    return ALERT_CLASS_MAP[type] || 'alert-primary';
  }
  dismiss() {
    this.alertService.clearAlert();
  }
}
