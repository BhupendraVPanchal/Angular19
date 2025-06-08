import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AppSettings } from '../../appSettings';
import { CommonService } from '../../shared/services/common.service';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  constructor(
    private http: HttpClient,
    private app_settings: AppSettings,
    private commonService: CommonService
  ) { }

  public signUp(data: any): Observable<any> {
    return this.http.post(this.app_settings.MainApiPath + 'SignUp/userSignUp', data);
  }

  public signIn(data: any): Observable<any> {
    return this.http.post(this.app_settings.MainApiPath + 'SignIn/userSignIn', data);
  }

  public storeAuthData(response: any): void {
    this.commonService.setItem('x-access-token', response.data[0].Token);
    this.commonService.setItem('user_id', response.data[0].userid.toString());
    response.remember_me? this.commonService.setItem('user_name',response.data[0].user_name):this.commonService.removeItem('user_name');
    response.remember_me? localStorage.setItem('password', response.password): localStorage.removeItem('password');
    response.remember_me? localStorage.setItem('remember_me', 'true'): localStorage.setItem('remember_me', 'false');
    this.commonService.setItem('token_expiry', response.data[0].token_expiry);
  }

  public clearAuthData(): void {
    this.commonService.removeItem('token');
    this.commonService.removeItem('user_id');
    this.commonService.removeItem('user_name');
    this.commonService.removeItem('token_expiry');
  }
}
