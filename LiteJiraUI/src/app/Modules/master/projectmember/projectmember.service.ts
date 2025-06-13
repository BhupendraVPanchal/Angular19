import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { AppSettings } from '../../../appSettings';
import { Router } from '@angular/router';


@Injectable({
  providedIn: 'root'
})
export class projectmemberService {


  public controller_name: string = "projectmember";


  constructor(public http: HttpClient, private router: Router, private app_settings: AppSettings) { }


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


}
