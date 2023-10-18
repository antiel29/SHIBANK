import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import {MatToolbarModule} from '@angular/material/toolbar'; 
import { RouterModule } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';

import { NotificationComponent } from './notification/notification.component';
import { NotificationService } from './notification/notification.service';
import { HeaderComponent } from './header/header.component';

@NgModule({
  declarations: [NotificationComponent, HeaderComponent],
  imports: [
    CommonModule,
    MatSnackBarModule,
    MatToolbarModule,
    RouterModule,
    MatButtonModule,
  ],
  exports:[NotificationComponent,HeaderComponent],
  providers:[NotificationService]
})
export class SharedModule { }
