import { Injectable } from '@angular/core';
import { HttpClient} from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class RegistrationService {
  private baseUrl = 'https://localhost:7150';

  constructor(private http: HttpClient) { }

  registerUser(user:any)
  {
    return this.http.post(`${this.baseUrl}/api/User/register`,user);
  }
}
