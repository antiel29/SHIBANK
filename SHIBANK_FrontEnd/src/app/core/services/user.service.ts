import { Injectable } from '@angular/core';
import { HttpClient} from '@angular/common/http'
import { User } from '../models/user.model';
import { UserUpdate } from '../models/user-update.model';
import {map} from 'rxjs/operators'

@Injectable
({
  providedIn: 'root'
})

export class UserService 
{
  private baseUrl = 'https://localhost:7150';
  constructor(private http: HttpClient) { }

  registerUser(user:User)
  {
    return this.http.post(`${this.baseUrl}/api/users/register`,user);
  }

  getUser()
  {
    return this.http.get(`${this.baseUrl}/api/users/current`).pipe(map((response: any ) =>
    {
      const user = new User();
      user.username = response.username;
      user.password = response.password;
      user.firstName = response.firstName;
      user.lastName = response.lastName;
      user.email = response.email;

      return user;
    }));
  }
  
  getUsers()
  {
    return this.http.get(`${this.baseUrl}/api/users`);
  }

  updateUser(user:UserUpdate)
  {
    return this.http.put(`${this.baseUrl}/api/users/current/update`,user);
  }
}
  
