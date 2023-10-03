import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { UserLogin } from '../models/user-login.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private baseUrl = 'https://localhost:7150';
  private authToken = 'authToken'

  constructor(private http:HttpClient) { }

  login(user:UserLogin): Observable<any>{
    return this.http.post(`${this.baseUrl}/api/Auth/login`,user);
  }
  
  storeToken(token: string):void{
    localStorage.setItem(this.authToken,token);
  }

  getToken(): string | null{
    return localStorage.getItem(this.authToken);
  }

  logout(): void{
    localStorage.removeItem(this.authToken);
  }

  isAuthenticated():boolean{
    const token = this.getToken();
    return !!token;
  }
}
