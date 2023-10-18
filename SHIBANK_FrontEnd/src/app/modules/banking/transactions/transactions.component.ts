import { Component, OnInit} from '@angular/core';
import { Account } from 'src/app/core/models/account.model';
import { AccountService } from 'src/app/core/services/account.service';
import { UserService } from 'src/app/core/services/user.service';
import { UserSimple } from 'src/app/core/models/user-simple.model';
import { TransactionCreate } from 'src/app/core/models/transaction-create.model';
import { TransactionService } from 'src/app/core/services/transaction.service';
import { NotificationService } from 'src/app/core/shared/notification/notification.service';

@Component({
  selector: 'app-transactions',
  templateUrl: './transactions.component.html',
  styleUrls: ['./transactions.component.css']
})
export class TransactionsComponent {
  selectedAccountOrigin: Account = new Account;
  selectedAccountDestiny: Account = new Account;
  selectedUser:UserSimple = new UserSimple;
  newTransfer:TransactionCreate = new TransactionCreate;
  message:string = '';
  amount: number = 0.0;
  users: UserSimple[] = [];
  accountsOrigin: Account[] = [];
  accountsDestiny: Account[] = [];

  constructor(private transactionService:TransactionService,private UserService:UserService,private accountService:AccountService,private notificationService:NotificationService){}

  ngOnInit() {
    this.getUserAccounts();
    this.getUsers();
  }

  getUserAccounts(){
    this.accountService.getUserAccounts().subscribe(
      (accounts) => {
        this.accountsOrigin = accounts;
        console.log(accounts);
      },
      (err) => {
        this.notificationService.openSnackBar('Error trying load accounts', 5000);
        console.error(err);
      }
    );
  }

  getAccountsByUserId(){
    this.accountService.getAccountsByUserId(this.selectedUser.id).subscribe(
      (accounts) => {
        this.accountsDestiny = accounts;
        console.log(accounts);
      },
      (err) => {
        this.notificationService.openSnackBar('Error trying load accounts', 5000);
        console.error(err);
      }
    );
  }


  getUsers(){
    this.UserService.getUsers().subscribe(
      (users) =>{
        this.users = <UserSimple[]>users;
        console.log(users);
      },
      (err) =>{
        this.notificationService.openSnackBar('Error trying load users', 5000);
        console.error(err);
      }
    );
  }

  onAccountSelectionOrigin(account: Account) {
    this.selectedAccountOrigin = account;
  }
  onAccountSelectionDestiny(account: Account) {
    this.selectedAccountDestiny = account;
  }
  onUserSelection(user:UserSimple){
    this.selectedUser = user;
    this.getAccountsByUserId();
  }

  createTransaction(){
    this.newTransfer.amount = this.amount;
    this.newTransfer.destinyAccountNumber = this.selectedAccountDestiny.accountNumber;
    this.newTransfer.originAccountNumber = this.selectedAccountOrigin.accountNumber;
    this.newTransfer.message = this.message;

    this.transactionService.createTransaction(this.newTransfer).subscribe(
      (res) =>{
        this.notificationService.openSnackBar('Success.', 3000);
        console.log(res);
      },
      (err) =>{
        this.notificationService.openSnackBar('Error.', 5000);
        console.error(err);
      }
    );
  }


}
