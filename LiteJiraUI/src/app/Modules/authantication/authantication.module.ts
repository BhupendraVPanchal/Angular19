import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { SignInComponent } from './sign-in/sign-in.component';
import { SignUpComponent } from './sign-up/sign-up.component';
import { MaterialModule } from '../../shared/utils/material/material.module';
import { LiteBoxComponent } from '../../shared/utils/reusable-components/lite-box/lite-box.component';
import { AuthanticationRoutingModule } from './authantication-routing.module';
import { AuthenticationService } from './authentication.service';
import { LiteCheckboxComponent } from '../../shared/utils/reusable-components/lite-checkbox/lite-checkbox.component';

@NgModule({
  declarations: [SignInComponent, SignUpComponent],
  imports: [
    CommonModule,
    HttpClientModule,
    MaterialModule,
    LiteBoxComponent,
    LiteCheckboxComponent,
    AuthanticationRoutingModule
  ],
  providers: [AuthenticationService]
})
export class AuthanticationModule { }
