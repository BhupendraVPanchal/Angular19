import { Component, Inject, OnInit, PLATFORM_ID } from '@angular/core';
import { CommonModule, NgClass, ViewportScroller, isPlatformBrowser } from '@angular/common';
import { RouterOutlet, Router, Event, NavigationEnd } from '@angular/router';
import { ToggleService } from './shared/common/sidebar/toggle.service';
import { CustomizerSettingsService } from './shared/customizer-settings/customizer-settings.service';
import { AlertService } from './shared/utils/alerts/alert-service.service';
import { CommonAlertsComponent } from './shared/utils/alerts/common-alerts/common-alerts.component';


@Component({
  selector: 'app-root',
  imports: [RouterOutlet, CommonModule, CommonAlertsComponent], 
    templateUrl: './app.component.html',
    styleUrl: './app.component.scss'
})

export class AppComponent implements OnInit {
    showAlert() {
        if (isPlatformBrowser(this.platformId)) {
            alert('This is a browser alert!');
        }
    }
    
    // Title
    title = 'Angular 19';

    // isSidebarToggled
    isSidebarToggled = false;

    constructor(
        public router: Router,
        private alertService: AlertService,
        private toggleService: ToggleService,
        private viewportScroller: ViewportScroller,
        public themeService: CustomizerSettingsService,
        @Inject(PLATFORM_ID) private platformId: object
    ) {
        this.router.events.subscribe((event: Event) => {
            if (event instanceof NavigationEnd) {
                this.viewportScroller.scrollToPosition([0, 0]);
            }
        });
        this.toggleService.isSidebarToggled$.subscribe(isSidebarToggled => {
            this.isSidebarToggled = isSidebarToggled;
        });
        }
        ngOnInit(): void {
  }
   
}
