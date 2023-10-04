import { Component } from '@angular/core';

import { UserLogin } from 'src/app/core/models/user-login.model';
import { AuthService } from 'src/app/core/authentication/auth.service';
import { Router } from '@angular/router';

import { NotificationService } from 'src/app/core/shared/notification/notification.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  user : UserLogin = new UserLogin();

  constructor(private router:Router,private authService:AuthService, private notificationService:NotificationService){}

  onSubmit(){
    if (this.user.form.valid){
      this.authService.login(this.user.form.value).subscribe(
      (res) =>{
        this.notificationService.openSnackBar('Successful login.',3000);
        console.log(res);
        this.authService.storeToken(res.token);
        
        this.router.navigate(['/home']);

      },
      (err) =>{
        this.notificationService.openSnackBar('Username dont exist or dont match with password.',3000);
        console.log(err);
      }
      );
    }
    else{
      this.notificationService.openSnackBar('Please fill the camps.',5000);
    }
  }
}
