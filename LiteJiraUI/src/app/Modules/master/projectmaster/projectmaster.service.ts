import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { AppSettings } from '../../../appSettings';
import { Router } from '@angular/router';


@Injectable({
providedIn: 'root'
})
export class projectmasterService
{


public controller_name: string = "projectmaster";


  constructor(public http: HttpClient, private router: Router, private app_settings: AppSettings) { }


public get_projectmaster_data(query: any) :  Observable<any> {
  return this.http.post(this.app_settings.MainApiPath + this.controller_name + '/get_projectmaster_data', query);
}


public add_update_projectmaster(query: any) :  Observable<any> {
return this.http.post(this.app_settings.MainApiPath + this.controller_name + '/add_update_projectmaster', query);
}


public get_projectmaster(query: any) :  Observable<any> {
return this.http.post(this.app_settings.MainApiPath + this.controller_name + '/get_projectmaster', query);
}


public delete_projectmaster(query: any) :  Observable<any> {
return this.http.post(this.app_settings.MainApiPath + this.controller_name + '/delete_projectmaster', query);
}


}
