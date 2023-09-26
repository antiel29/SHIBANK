import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {

  constructor(private snackBar: MatSnackBar) { }

  openSnackBar(message: string, duration: number) {
    this.snackBar.open(message, 'Close', {
      duration: duration,
      panelClass: 'custom-snackbar'
    });
}
}
