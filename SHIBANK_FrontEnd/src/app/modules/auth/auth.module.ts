import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { RegisterComponent } from './register/register.component';
import { AuthRoutingModule } from './auth-routing.module';


@NgModule({
  declarations: [RegisterComponent],
  imports: [
    FormsModule,
    CommonModule,
    AuthRoutingModule,
  ]
})
export class AuthModule { }
