import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AppSettings } from '../../appSettings';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ComponyService {

  constructor(private http :HttpClient,private app_settings : AppSettings) { }
  public SaveCompanyDetails(data:any): Observable<any> {
    return this.http.post(this.app_settings.MainApiPath + 'CompanyMaster/SaveCompanyDetails', data);
  }
  public getCompanyDetails(data:any): Observable<any> {
    
    return this.http.post(this.app_settings.MainApiPath + 'CompanyMaster/getCompanyDetails', data);
  }

}
