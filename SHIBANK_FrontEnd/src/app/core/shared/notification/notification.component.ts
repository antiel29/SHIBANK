import { Component } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { NotificationService } from './notification.service';

@Component({
  selector: 'app-notification',
  templateUrl: './notification.component.html',
  styleUrls: ['./notification.component.css']
})
export class NotificationComponent {
  constructor(private notificationService: NotificationService){}

  openSnackBar(message: string, duration: number) {
    this.notificationService.openSnackBar(message, duration);
  }
}

