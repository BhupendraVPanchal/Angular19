import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { AppSettings } from '../../../appSettings';
import { Router } from '@angular/router';


@Injectable({
providedIn: 'root'
})
export class task_documentService
{


public controller_name: string = "task_document";


constructor(public http: HttpClient, private router: Router, private app_settings: AppSettings) { }


public get_task_document_data(query: any) :  Observable<any> {
return this.http.post(this.app_settings.MainApiPath + this.controller_name + '/get_task_document_data', query);
}


public add_update_task_document(query: any) :  Observable<any> {
return this.http.post(this.app_settings.MainApiPath + this.controller_name + '/add_update_task_document', query);
}


public get_task_document(query: any) :  Observable<any> {
return this.http.post(this.app_settings.MainApiPath + this.controller_name + '/get_task_document', query);
}


public delete_task_document(query: any) :  Observable<any> {
return this.http.post(this.app_settings.MainApiPath + this.controller_name + '/delete_task_document', query);
}


}
