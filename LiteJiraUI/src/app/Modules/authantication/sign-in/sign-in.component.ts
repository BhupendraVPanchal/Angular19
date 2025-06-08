import { Component, Inject, PLATFORM_ID } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { isPlatformBrowser } from '@angular/common';
import { Router } from '@angular/router';
import { CustomizerSettingsService } from '../../../shared/customizer-settings/customizer-settings.service';
import { AuthenticationService } from '../authentication.service';
import { CommonService } from '../../../shared/services/common.service';
import { AlertService } from '../../../shared/utils/alerts/alert-service.service';
import { AlertType } from '../../../shared/utils/alerts/alert-types';

@Component({
    selector: 'app-sign-in',
    standalone: false,
    templateUrl: './sign-in.component.html',
    styleUrl: './sign-in.component.scss'
})
export class SignInComponent {
  public isBrowser: any;
  public hide = true;
  public authForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    @Inject(PLATFORM_ID) private platformId: Object,
    public themeService: CustomizerSettingsService,
    private authService: AuthenticationService,
    private commonService: CommonService,
    private alertService: AlertService,
    private router: Router
  ) {
    this.isBrowser = isPlatformBrowser(this.platformId);
    this.authForm = this.fb.group({
      username: [localStorage.getItem('user_name'), [Validators.required]],
      password: [localStorage.getItem('password'), [Validators.required, Validators.minLength(8)]],
      remember_me: [localStorage.getItem('remember_me')??false]
    });
  }

  login() {
    const obj = {
      username: this.authForm.value.username,
      password: this.authForm.value.password
    };

    if (this.authForm.valid) {
      this.authService.signIn(obj).subscribe({
        next: (res) => {
          console.log( "ergrewgewge",res);
          if (res.data ) {
            this.alertService.showAlert(res.data[0].message.toString(), AlertType.BG_PRIMARY);
            this.authForm.value.remember_me ? res["remember_me"] = true : res["remember_me"] = false;
            this.authForm.value.remember_me ? res["password"]=this.authForm.value.password : res["password"]='';
            this.authService.storeAuthData(res);
            this.router.navigate(['']);
          } else {
            this.alertService.showAlert('Invalid credentials', AlertType.DANGER);
          }
        },
        error: (err) => {
          this.alertService.showAlert('Login failed. Please try again.', AlertType.DANGER);
        }
      });
    } else {
      this.alertService.showAlert('Please fill all required fields correctly', AlertType.WARNING);
    }
  }
}
