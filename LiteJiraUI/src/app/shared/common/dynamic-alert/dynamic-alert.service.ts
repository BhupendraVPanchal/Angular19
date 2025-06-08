import { Injectable } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { DynamicAlertComponent } from './dynamic-alert.component';

@Injectable({
  providedIn: 'root'
})
export class DynamicAlertService {
  constructor(private dialog: MatDialog) { }

  fire(obj: dynamic_alert_input): Promise<dynamic_alert_output> {

    let dialogConfig = new MatDialogConfig<dynamic_alert_input>();
    dialogConfig.data = obj;
    dialogConfig.enterAnimationDuration = 300;
    dialogConfig.exitAnimationDuration = 150;
    if (obj.width) {
      dialogConfig.width = String(obj.width) + 'px';
    }
    if (obj.height) {
      dialogConfig.height = String(obj.height) + 'px';
    }

    if (obj.position) {
      var _size = '20px';
      if (obj.position == 'top-start') {
        dialogConfig.position = {
          top: _size,
          left: _size,
        }
      }
      else if (obj.position == 'top-end') {
        dialogConfig.position = {
          top: _size,
          right: _size,
        }
      }
      else if (obj.position == 'bottom-start') {
        dialogConfig.position = {
          bottom: _size,
          left: _size,
        }
      }
      else if (obj.position == 'bottom-end') {
        dialogConfig.position = {
          bottom: _size,
          right: _size,
        }
      }
    }

    dialogConfig.panelClass = "pop-alert";
    //dialogConfig.hasBackdrop = (obj.allowOutsideClick == null) ? true : obj.allowOutsideClick;
    dialogConfig.disableClose = (obj.allowOutsideClick == null) ? false : !obj.allowOutsideClick;

    return new Promise((resolve, reject) => {
      try {
        var dialog = this.dialog.open(DynamicAlertComponent, dialogConfig);
        dialog.afterClosed().subscribe((data: dynamic_alert_output) => {
          resolve(data);
        });
      }
      catch (ex: any) {
        reject(ex.message);
      }
    });

  }
}
export interface dynamic_alert_input {
  /**
   * The main title of the alert , this property is compulsory.
   */
  title: string;

  /**
   * The description of the alert.
   */
  text?: string;

  /**
   * Whether to show confirm button or not , default is 'true' for this property.
   */
  showConfirmButton?: boolean;

  /**
   * Whether to show cancel button or not.
   */
  showCancelButton?: boolean;

  /**
   * Whether to show deny button or not.
   */
  showDenyButton?: boolean;

  /**
   * Whether to show close button or not.
   */
  showCloseButton?: boolean;

  /**
   * Specify confirm button text.
   */
  confirmButtonText?: string;

  /**
   * Specify cancel button text.
   */
  cancelButtonText?: string;

  /**
   * Specify deny button text.
   */
  denyButtonText?: string;

  /**
   * Specify time in 'milliseconds' after which alert will be automatically dismissed.
   */
  timer?: number;

  /**
   * Specify position of the alert ,default value is 'center' for this property.
   */
  position?: alert_position;

  /**
   * Specify width of the alert in px.
   */
  width?: number;

  /**
   * Specify height of the alert in px.
   */
  height?: number;

  /**
   * Specify the icon type
   */
  icon?: icon_type

  /**
   * Whether user can dismiss the popup by clicking outside it,default is 'true'
   */
  allowOutsideClick?: boolean

  showInput?: boolean;  // New property to show the input field

  inputLabel?: string;  // New property for the input label

}

type alert_position =
  | 'top-start'
  | 'top-end'
  | 'center'
  | 'bottom-start'
  | 'bottom-end'

type icon_type =
  | 'error'
  | 'info'
  | 'question'
  | 'success'
  | 'warning'

export interface dynamic_alert_output {
  /**
   * shows whether user clicked on confirm button or not.
   */
  isConfirmed: boolean;

  /**
   * shows whether user clicked on deny button or not.
   */
  isDenied: boolean;

  /**
   * shows whether the alert is dismissed or not.
   */
  isDismissed: boolean;

  /**
   * if alert is dismissed , it gives the dismissed reason.
   */
  dismiss: dismiss_reason;

  userInput?: string;  // New property to include user input
}

export enum dismiss_reason {
  /**
   * When user dismiss the alert ,by clicking outside the alert modal.
   */
  backdrop,

  /**
   * When user dismiss the alert ,by clicking cancel button.
   */
  cancel,

  /**
   * When user dismiss the alert ,by clicking close button.
   */
  close,

  /**
   * When alert is dismissed due to timer specified by user ran out.
   */
  timer_end,

  /**
   * When userInput by user.
   */
  userInput,
}

