import { Component } from '@angular/core';

import { UserService } from 'src/app/core/services/user.service';
import { UserRegister } from 'src/app/core/models/user-register.model';

import { NotificationService } from 'src/app/core/shared/notification/notification.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  user : UserRegister = new UserRegister ();

  constructor(private userService: UserService, private notificationService: NotificationService){}

  onSubmit() {
    if(this.user.form.valid){
    this.userService.registerUser(this.user.form.value).subscribe(
      (res) =>{
        this.notificationService.openSnackBar('Successful registration.',3000);
        console.log(res);
      },
      (err) => {
        this.notificationService.openSnackBar('Error while attempting to register. Please verify the data and try again.',5000);
        console.error(err);
      } 
      );
    }
    else{
      this.notificationService.openSnackBar('Invalid form data. Please check your inputs',5000);
    }
  }
  }  
