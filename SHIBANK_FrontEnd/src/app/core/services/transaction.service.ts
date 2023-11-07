import { Injectable } from '@angular/core';
import { HttpClient} from '@angular/common/http'
import { TransactionCreate } from '../models/transaction-create.model';
import { map } from 'rxjs';

@Injectable
({
  providedIn: 'root'
})
export class TransactionService 
{
  private baseUrl = 'https://localhost:7150';
  constructor(private http: HttpClient){}

  getTransactions() 
  {
    return this.http.get(`${this.baseUrl}/api/transactions`).pipe(map((data: any) =>
     {
        if (Array.isArray(data)) 
        {
          return data;
        } 
        else 
        {
          return [data];
        }
      }));
    }

  createTransaction(transaction:TransactionCreate)
  {
    return this.http.post(`${this.baseUrl}/api/transactions`,transaction)
  }
}
