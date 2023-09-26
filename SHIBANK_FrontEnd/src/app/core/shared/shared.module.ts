import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatSnackBarModule } from '@angular/material/snack-bar';

import { NotificationComponent } from './notification/notification.component';
import { NotificationService } from './notification/notification.service';

@NgModule({
  declarations: [NotificationComponent],
  imports: [
    CommonModule,
    MatSnackBarModule
  ],
  exports:[NotificationComponent],
  providers:[NotificationService]
})
export class SharedModule { }
