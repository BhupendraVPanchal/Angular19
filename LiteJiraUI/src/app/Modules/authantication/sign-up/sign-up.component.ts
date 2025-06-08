import { Component, Inject, OnInit, PLATFORM_ID } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { CustomizerSettingsService } from '../../../shared/customizer-settings/customizer-settings.service';
import {  isPlatformBrowser } from '@angular/common';
import { AuthenticationService } from '../authentication.service';
@Component({
    selector: 'app-sign-up',
    standalone:false,
    templateUrl: './sign-up.component.html',
    styleUrl: './sign-up.component.scss'
})
export class SignUpComponent implements OnInit{
   public isBrowser: boolean;
   public hide = true;
   public  authForm: FormGroup;

    constructor(
        private fb: FormBuilder,
        @Inject(PLATFORM_ID) private platformId: Object,
        private router: Router,
        public themeService: CustomizerSettingsService,
        private authService : AuthenticationService
    ) {
        this.isBrowser = isPlatformBrowser(this.platformId); 
        this.authForm = this.fb.group({
            name: ['', Validators.required],
            email: ['', [Validators.required, Validators.email]],
            password: ['', [Validators.required, Validators.minLength(8)]],
            confirmPassword: ['', Validators.required], 
        });
    }
    ngOnInit(): void {
    }

    onSubmit() {
        if (this.authForm.valid) {
            this.authService.signUp(this.authForm.value).subscribe((res:any) => {
                alert()
                console.log(res);
            })
        } else {
            console.log('Form is invalid. Please check the fields.');
        }
    }

}
