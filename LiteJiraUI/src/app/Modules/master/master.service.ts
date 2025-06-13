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
    return this.http.post(this.app_settings.MainApiPath + this.controller_name + '/add_update_companymaster', query);
  }


  public get_companymaster(query: any): Observable<any> {
    return this.http.post(this.app_settings.MainApiPath + this.controller_name + '/get_companymaster', query);
  }


  public delete_companymaster(query: any): Observable<any> {
    return this.http.post(this.app_settings.MainApiPath + this.controller_name + '/delete_companymaster', query);
  }

  public get_companymaster_help(query: any): Observable<any> {
    return this.http.post(this.app_settings.MainApiPath + this.controller_name + '/get_companymaster_help', query);
  }

  public get_designation_help(query: any): Observable<any> {
    return this.http.post(this.app_settings.MainApiPath + this.controller_name + '/get_designation_help', query);
  }


  // Project Member

  public get_projectmember_data(query: any): Observable<any> {
    return this.http.post(this.app_settings.MainApiPath + this.controller_name + '/get_projectmember_data', query);
  }


  public add_update_projectmember(query: any): Observable<any> {
    return this.http.post(this.app_settings.MainApiPath + this.controller_name + '/add_update_projectmember', query);
  }


  public get_projectmember(query: any): Observable<any> {
    return this.http.post(this.app_settings.MainApiPath + this.controller_name + '/get_projectmember', query);
  }


  public delete_projectmember(query: any): Observable<any> {
    return this.http.post(this.app_settings.MainApiPath + this.controller_name + '/delete_projectmember', query);
  }

  // Project Master
  public get_projectmaster_data(query: any): Observable<any> {
    return this.http.post(this.app_settings.MainApiPath + this.controller_name + '/get_projectmaster_data', query);
  }


  public add_update_projectmaster(query: any): Observable<any> {
    return this.http.post(this.app_settings.MainApiPath + this.controller_name + '/add_update_projectmaster', query);
  }


  public get_projectmaster(query: any): Observable<any> {
    return this.http.post(this.app_settings.MainApiPath + this.controller_name + '/get_projectmaster', query);
  }


  public delete_projectmaster(query: any): Observable<any> {
    return this.http.post(this.app_settings.MainApiPath + this.controller_name + '/delete_projectmaster', query);
  }




}
