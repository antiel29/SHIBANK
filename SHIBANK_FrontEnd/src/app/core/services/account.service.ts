import { Injectable } from '@angular/core';
import { HttpClient} from '@angular/common/http'
import { Account } from '../models/account.model';
import { Observable } from 'rxjs';

@Injectable
({
  providedIn: 'root'
})
export class AccountService 
{
  private baseUrl = 'https://localhost:7150';
  constructor(private http: HttpClient){}

  getUserAccounts():Observable<Account[]>
  {
    return this.http.get<Account[]>(`${this.baseUrl}/api/bank-accounts/current`);
  }

  getAccountsByUserId(id:number):Observable<Account[]>
  {
    return this.http.get<Account[]>(`${this.baseUrl}/api/bank-accounts/user/${id}`);
  }

  createAccount()
  {
    return this.http.post(`${this.baseUrl}/api/bank-accounts/current/create`,{});
  }

  deleteAccount(type:string)
  {
    return this.http.delete(`${this.baseUrl}/api/bank-accounts/${type}/delete`);
  }

  deposit(type:string,amount:number)
  {
    return this.http.put(`${this.baseUrl}/api/BankAccount/deposit/${type}?amount=${amount}`, null)
  }
}
