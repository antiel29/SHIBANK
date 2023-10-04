import { Component } from '@angular/core';
import { AuthService } from 'src/app/core/authentication/auth.service';

import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  constructor(private router:Router,private authService: AuthService) {}

  logout() {
    this.authService.logout();
    this.router.navigate(['/auth/login']);
  }
}
