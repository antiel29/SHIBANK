import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { BankingRoutingModule } from './banking-routing.module';
import { AccountsComponent } from './accounts/accounts.component';
import { ProfileComponent } from './profile/profile.component';
import { TransactionsComponent } from './transactions/transactions.component';

import { SharedModule } from 'src/app/core/shared/shared.module';
import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
    declarations: [AccountsComponent,ProfileComponent,TransactionsComponent],
    imports: [
      ReactiveFormsModule,
      FormsModule,
      SharedModule,
      CommonModule,
      BankingRoutingModule,
    ]
  })
  export class BankingModule { }