import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../authentication/auth.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent {
  constructor(private router: Router, private authService:AuthService){}

  profile(){
    this.router.navigate(['/banking/profile']);
  }
  logout() {
    this.authService.logout();
    this.router.navigate(['/auth/login']);
  }

  shouldShowHeader(): boolean {
    return !['/auth/login', '/auth/register'].includes(this.router.url);
  }
}
