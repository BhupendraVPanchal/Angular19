import {  Injectable } from "@angular/core";
@Injectable({
  providedIn: 'root'
})
export class AppSettings{
  public MainApiPath: string = "https://localhost:7273/api/";
  public fileViewUrl: string = 'https://dev.letsgoa.co.in/admin/API/api/';

}
