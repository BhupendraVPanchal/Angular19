import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { AppSettings } from '../../appSettings';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MasterService {

  constructor(public http: HttpClient, private app_settings: AppSettings, private router: Router) { }

  public controller_name: string = "master";


  // Company Master

  public get_companymaster_data(query: any): Observable<any> {
    return this.http.post(this.app_settings.MainApiPath + this.controller_name + '/get_companymaster_data', query);
  }

  public add_update_companymaster(query: any): Observable<any> {
    return this.http.post(this.app_settings.MainApiPath  + this.controller_name + '/add_update_companymaster', query);
  }


  public get_companymaster(query: any): Observable<any> {
    return this.http.post(this.app_settings.MainApiPath  + this.controller_name + '/get_companymaster', query);
  }


  public delete_companymaster(query: any): Observable<any> {
    return this.http.post(this.app_settings.MainApiPath  + this.controller_name + '/delete_companymaster', query);
  }


  // Project Master

  


}
