import { isPlatformBrowser } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Inject, Injectable, PLATFORM_ID } from '@angular/core';
import { Observable } from 'rxjs';
import { AppSettings } from '../../appSettings';
import { FormControl, FormGroup } from '@angular/forms';

@Injectable({
  providedIn: 'root'
})
export class CommonService {
  private isBrowser: boolean;


  constructor(@Inject(PLATFORM_ID) private platformId: Object,private http :HttpClient,private app_settings: AppSettings) {
    this.isBrowser = isPlatformBrowser(platformId);
  }
  public set_cookie(name: string, value: string, expire_days: number, path: string = '') {
    let d: Date = new Date();
    d.setTime(d.getTime() + expire_days * 24 * 60 * 60 * 1000);
    let expires: string = `expires=${d.toUTCString()}`;
    let cpath: string = path ? `; path=${path}` : '';
    document.cookie = `${name}=${value}; ${expires}${cpath}`;
  }

  public get_cookie(name: string) {
    let ca: Array<string> = document.cookie.split(';');
    let caLen: number = ca.length;
    let cookieName = `${name}=`;
    let c: string;

    for (let i: number = 0; i < caLen; i += 1) {
      c = ca[i].replace(/^\s+/g, '');
      if (c.indexOf(cookieName) == 0) {
        return c.substring(cookieName.length, c.length);
      }
    }
    return '';
  }


  setItem(key: string, value: string): void {
    if (this.isBrowser) {
      localStorage.setItem(key, value);
    }
  }

  getItem(key: string): string | null {
    if (this.isBrowser) {
      return localStorage.getItem(key);
    }
    return null;
  }

  removeItem(key: string): void {
    if (this.isBrowser) {
      localStorage.removeItem(key);
    }
  }

  clear(): void {
    if (this.isBrowser) {
      localStorage.clear();
    }
  }

  public GetLocationData(data:any):Observable<any>{
  return this.http.post(this.app_settings.MainApiPath + 'CompanyMaster/GetLocationData',data);
  }

  public cloneForm(originalForm: FormGroup): FormGroup {
    const clonedForm = new FormGroup({});
    Object.keys(originalForm.controls).forEach(controlName => {
      const control = originalForm.controls[controlName];
      clonedForm.addControl(controlName, new FormControl(control.value));
    });
    return clonedForm;
  }

  public getDirectoryStructureget(): Observable<any> {
    return this.http.get(this.app_settings.MainApiPath + 'Directory/structure');
  }
  public getDirectoryStructure(data: any): Observable<any> {
    return this.http.post(this.app_settings.MainApiPath + 'Directory/structurepost', data);
  }
  public Download(data: any): Observable<any> {
    return this.http.post(this.app_settings.MainApiPath + 'Directory/createzip', data, { responseType: 'blob' });
  }

  
}
