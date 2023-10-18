import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { BankingRoutingModule } from './banking-routing.module';
import { AccountsComponent } from './accounts/accounts.component';
import { ProfileComponent } from './profile/profile.component';
import { TransactionsComponent } from './transactions/transactions.component';

import { SharedModule } from 'src/app/core/shared/shared.module';
import { MatButtonModule } from '@angular/material/button';
import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import {MatInputModule} from '@angular/material/input'
import {MatSelectModule} from '@angular/material/select'; 


@NgModule({
    declarations: [AccountsComponent,ProfileComponent,TransactionsComponent],
    imports: [
      ReactiveFormsModule,
      FormsModule,
      SharedModule,
      MatButtonModule,
      MatInputModule,
      MatSelectModule,
      CommonModule,
      BankingRoutingModule,
    ]
  })
  export class BankingModule { }