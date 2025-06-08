import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private tokenKey = 'x-access-token';

  constructor(private router: Router) {}

  setToken(token: string): void {
    localStorage.setItem(this.tokenKey, token);
  }
  getToken(): string | null {
    const expiry = localStorage.getItem('token_expiry');
    
    if (expiry && new Date(expiry) > new Date()) {
      return localStorage.getItem(this.tokenKey);
    }
  
    return null;
  }
  
  clearToken(): void {
    localStorage.removeItem(this.tokenKey);
    this.router.navigate(['/login']);
  }

  isAuthenticated(): boolean {
    return !!this.getToken(); 
  }
}
