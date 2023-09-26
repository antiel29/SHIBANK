import { Injectable } from '@angular/core';
import { HttpClient} from '@angular/common/http'
import { UserRegister } from '../models/user-register.model';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private baseUrl = 'https://localhost:7150';
  constructor(private http: HttpClient) { }

  registerUser(user:UserRegister){
    return this.http.post(`${this.baseUrl}/api/User/register`,user);
  }
}
