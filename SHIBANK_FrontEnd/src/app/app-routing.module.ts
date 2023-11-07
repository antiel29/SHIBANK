import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {RouterModule, Routes} from '@angular/router'
import { AuthGuard } from './core/authentication/auth-guard.service';
import { HomeComponent } from './modules/home/home.component';
import { AuthGuardHome } from './core/authentication/auth-guard-home.service';

const routes: Routes = 
[
  {path : 'auth',
  canActivate:[AuthGuardHome],
   loadChildren: () => import('./modules/auth/auth.module').then(m => m.AuthModule)
  },
  {path : 'banking',
  canActivate:[AuthGuard],
   loadChildren: () => import('./modules/banking/banking.module').then(m => m.BankingModule)
  },
  {path : '' , redirectTo: 'auth/login',pathMatch: 'full'},
  {path: 'home', component:HomeComponent, canActivate:[AuthGuard]},
  {path: '**', redirectTo: 'home'},
]

@NgModule
({
  declarations: [],
  imports: 
  [
    CommonModule,
    RouterModule.forRoot(routes),
  ],
  exports:
  [
    RouterModule,
  ]
})
export class AppRoutingModule { }
