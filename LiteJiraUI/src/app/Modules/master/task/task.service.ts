import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { AppSettings } from '../../../appSettings';
import { Router } from '@angular/router';


@Injectable({
  providedIn: 'root'
})
export class taskService {


  public controller_name: string = "task";


  constructor(public http: HttpClient, private router: Router, private app_settings: AppSettings) { }

  public get_projectmaster_help(query: any): Observable<any> {
    return this.http.post(this.app_settings.MainApiPath + this.controller_name + '/get_projectmaster_help', query);
  }

  public get_tasktype_help(query: any): Observable<any> {
    return this.http.post(this.app_settings.MainApiPath + this.controller_name + '/get_tasktype_help', query);
  }

  public get_prioritymaster_help(query: any): Observable<any> {
    return this.http.post(this.app_settings.MainApiPath + this.controller_name + '/get_prioritymaster_help', query);
  }

  public get_tagmaster_help(query: any): Observable<any> {
    return this.http.post(this.app_settings.MainApiPath + this.controller_name + '/get_tagmaster_help', query);
  }

  public get_task_data(query: any): Observable<any> {
    return this.http.post(this.app_settings.MainApiPath + this.controller_name + '/get_task_data', query);
  }


  public add_update_task(query: any): Observable<any> {
    return this.http.post(this.app_settings.MainApiPath + this.controller_name + '/add_update_task', query);
  }


  public get_task(query: any): Observable<any> {
    return this.http.post(this.app_settings.MainApiPath + this.controller_name + '/get_task', query);
  }


  public delete_task(query: any): Observable<any> {
    return this.http.post(this.app_settings.MainApiPath + this.controller_name + '/delete_task', query);
  }


}
