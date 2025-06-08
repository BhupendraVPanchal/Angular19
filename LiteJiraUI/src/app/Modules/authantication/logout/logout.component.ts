import { Component } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { RouterLink } from '@angular/router';
import { CustomizerSettingsService } from '../../../shared/customizer-settings/customizer-settings.service';
import { AuthenticationService } from '../authentication.service';
import { Router } from '@angular/router';
@Component({
    selector: 'app-logout',
    imports: [RouterLink, MatButtonModule],
    templateUrl: './logout.component.html',
    styleUrl: './logout.component.scss'
})
export class LogoutComponent {

    constructor(
      public themeService: CustomizerSettingsService,
      private authService: AuthenticationService,
      private router: Router
    ) {
      authService.clearAuthData();
      this.router.navigate(['auth/sign-in']);
    }

}
