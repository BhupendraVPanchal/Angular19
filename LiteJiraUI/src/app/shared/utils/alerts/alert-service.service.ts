import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { AlertType } from './alert-types';

@Injectable({ providedIn: 'root' })
export class AlertService {
  private alertSubject = new BehaviorSubject<{ message: string; type: AlertType; timeout: number ; showConfirm: boolean} | null>(null);
  alert$ = this.alertSubject.asObservable();

  showAlert(message: string, type: AlertType = AlertType.PRIMARY,showConfirm: boolean=false, timeout: number = 5000) {
    this.alertSubject.next({ message, type, showConfirm,timeout });
    setTimeout(() => {
      this.clearAlert();
    }, !showConfirm ? timeout : 60000 );
  }

  clearAlert() {
    this.alertSubject.next(null);
  }
}
