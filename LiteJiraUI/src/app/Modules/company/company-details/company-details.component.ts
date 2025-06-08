import { Component, OnInit, Inject, PLATFORM_ID, TransferState, makeStateKey } from '@angular/core';
import { ComponyService } from '../compony.service';
import { isPlatformServer } from '@angular/common';

const COMPANY_DETAILS_KEY = makeStateKey<any>('companyDetails');

@Component({
  selector: 'app-company-details',
  standalone: false,
  templateUrl: './company-details.component.html',
  styleUrl: './company-details.component.scss'
})
export class CompanyDetailsComponent implements OnInit {
  public companyDetails: any;

  constructor(
    private companyService: ComponyService,
    private state: TransferState,
    @Inject(PLATFORM_ID) private platformId: Object
  ) {}

  ngOnInit(): void {
    this.getCompanyDetails();
  }

  private getCompanyDetails() {
    // Check if data already exists in TransferState (pre-fetched on SSR)
    if (this.state.hasKey(COMPANY_DETAILS_KEY)) {
      this.companyDetails = this.state.get(COMPANY_DETAILS_KEY, null);
    } else {
      this.companyService.getCompanyDetails({ companyID: 1 }).subscribe(res => {
        this.companyDetails = res.data[0];
        console.log(this.companyDetails);

        // Store data in TransferState (only if running on the server)
        if (isPlatformServer(this.platformId)) {
          this.state.set(COMPANY_DETAILS_KEY, this.companyDetails);
        }
      });
    }
  }

  tableColumns: any[] = [
    { key: 'image', label: 'Photo', type: 'image', filterable: false },
    { key: 'name', label: 'Name', type: 'text', filterable: true },
    { key: 'email', label: 'Email', type: 'text', filterable: true },
  ];

  tableData: any[] = [
    { image: 'https://coreldrawdesign.com/uploads/1666544429X_XTENTACION.jpg', name: 'Alice', email: 'alice@example.com' },
    { image: 'https://coreldrawdesign.com/uploads/1666544429X_XTENTACION.jpg', name: 'B', email: 'bob@example.com' },
    { image: 'https://coreldrawdesign.com/uploads/1666544429X_XTENTACION.jpg', name: 'C', email: 'C@example.com' },
    { image: 'https://coreldrawdesign.com/uploads/1666544429X_XTENTACION.jpg', name: 'D', email: 'D@example.com' },
    { image: 'https://coreldrawdesign.com/uploads/1666544429X_XTENTACION.jpg', name: 'E', email: 'bob@example.com' },
    { image: 'https://coreldrawdesign.com/uploads/1666544429X_XTENTACION.jpg', name: 'F', email: 'bob@example.com' },
    { image: 'https://coreldrawdesign.com/uploads/1666544429X_XTENTACION.jpg', name: 'G', email: 'bob@example.com' },
    { image: 'https://coreldrawdesign.com/uploads/1666544429X_XTENTACION.jpg', name: 'H', email: 'bob@example.com' },
    { image: 'https://coreldrawdesign.com/uploads/1666544429X_XTENTACION.jpg', name: 'I', email: 'bob@example.com' },
    { image: 'https://coreldrawdesign.com/uploads/1666544429X_XTENTACION.jpg', name: 'J', email: 'bob@example.com' },
    { image: 'https://coreldrawdesign.com/uploads/1666544429X_XTENTACION.jpg', name: 'K', email: 'bob@example.com' },
    { image: 'https://coreldrawdesign.com/uploads/1666544429X_XTENTACION.jpg', name: 'L', email: 'bob@example.com' },
    { image: 'https://coreldrawdesign.com/uploads/1666544429X_XTENTACION.jpg', name: 'M', email: 'bob@example.com' },
    { image: 'https://coreldrawdesign.com/uploads/1666544429X_XTENTACION.jpg', name: 'N', email: 'bob@example.com' },
    { image: 'https://coreldrawdesign.com/uploads/1666544429X_XTENTACION.jpg', name: 'O', email: 'bob@example.com' },
    { image: 'https://coreldrawdesign.com/uploads/1666544429X_XTENTACION.jpg', name: 'P', email: 'bob@example.com' },
    { image: 'https://coreldrawdesign.com/uploads/1666544429X_XTENTACION.jpg', name: 'Q', email: 'bob@example.com' },
    { image: 'https://coreldrawdesign.com/uploads/1666544429X_XTENTACION.jpg', name: 'R', email: 'bob@example.com' },

    { image: 'https://coreldrawdesign.com/uploads/1666544429X_XTENTACION.jpg', name: 'Bob', email: 'bob@example.com' },
    { image: 'https://coreldrawdesign.com/uploads/1666544429X_XTENTACION.jpg', name: 'Bob', email: 'bob@example.com' },
    { image: 'https://coreldrawdesign.com/uploads/1666544429X_XTENTACION.jpg', name: 'Bob', email: 'bob@example.com' },
    { image: 'https://coreldrawdesign.com/uploads/1666544429X_XTENTACION.jpg', name: 'Bob', email: 'bob@example.com' },
    { image: 'https://coreldrawdesign.com/uploads/1666544429X_XTENTACION.jpg', name: 'Bob', email: 'bob@example.com' },
    { image: 'https://coreldrawdesign.com/uploads/1666544429X_XTENTACION.jpg', name: 'Bob', email: 'bob@example.com' },
  ];
}
