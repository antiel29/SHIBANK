import { Component, OnInit} from '@angular/core';
import { Account } from 'src/app/core/models/account.model';
import { AccountService } from 'src/app/core/services/account.service';
import { NotificationService } from 'src/app/core/shared/notification/notification.service';

@Component({
  selector: 'app-accounts',
  templateUrl: './accounts.component.html',
  styleUrls: ['./accounts.component.css']
})
export class AccountsComponent implements OnInit {
  selectedAccount: Account = new Account;
  amount: number = 0.0;
  accounts: Account[] = [];

  constructor(private accountService:AccountService, private notificationService: NotificationService){}

  ngOnInit() {
      this.getUserAccounts();
  }

  getUserAccounts(){
    this.accountService.getUserAccounts().subscribe(
      (accounts) => {
        this.accounts = accounts;
        console.log(accounts);
      },
      (err) => {
        this.notificationService.openSnackBar('Error trying load accounts', 5000);
        console.error(err);
      }
    );
  }

  createAccount(){
    this.accountService.createAccount().subscribe(
      (res) =>{
        this.notificationService.openSnackBar('New account created.', 3000);
        console.log(res);
        this.getUserAccounts();
      },
      (err) =>{
        this.notificationService.openSnackBar('Error when trying to create an account, the limit is 5 accounts.', 5000);
        console.error(err);
      }
    );
  }

  deleteAccount(){
    this.accountService.deleteAccount(this.selectedAccount.accountNumber).subscribe(
      (res) =>{
        this.notificationService.openSnackBar('Account deleted.', 3000);
        console.log(res);
        this.getUserAccounts();
      },
      (err) =>{
        this.notificationService.openSnackBar('Error when trying to delete an account.', 5000);
        console.error(err);
      }
    );
  }

  deposit(){
    this.accountService.deposit(this.selectedAccount.accountNumber,this.amount).subscribe(
      (res) =>{
        this.notificationService.openSnackBar('Success.', 3000);
        console.log(res);
        this.amount = 0;
        this.getUserAccounts();
      },
      (err) =>{
        this.notificationService.openSnackBar('Amount has to be non-negative.', 5000);
        console.error(err);
      }
    );
  }

  onAccountSelection(account: Account) {
    this.selectedAccount = account;
  }
}
