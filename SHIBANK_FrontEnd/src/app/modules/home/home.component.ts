import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Transaction } from 'src/app/core/models/transaction.model';
import { TransactionService } from 'src/app/core/services/transaction.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  constructor(private transactionService:TransactionService){}

  formatDate(dateString: string): string {
    const date = new Date(dateString);
    return date.toLocaleString(); 
  }

  displayedColumns: string[] = [
  'originUsername',
  'destinyUsername',
  'amount',
  'message',
  'date',
];
  dataSource: MatTableDataSource<Transaction> = new MatTableDataSource<Transaction>([]);

  ngOnInit() {
    this.transactionService.getTransactions().subscribe((data) => {
      data.forEach((transaction) => {
        transaction.date = this.formatDate(transaction.date);
      });
      this.dataSource = new MatTableDataSource(data);
    });
  }
}
