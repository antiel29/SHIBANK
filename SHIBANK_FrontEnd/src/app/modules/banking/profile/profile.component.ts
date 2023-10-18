import { Component } from '@angular/core';
import { UserService } from 'src/app/core/services/user.service';
import { User } from 'src/app/core/models/user.model';
import { UserUpdate } from 'src/app/core/models/user-update.model';
import { NotificationService } from 'src/app/core/shared/notification/notification.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent {
  isEditMode = false;
  user: User = new User();

  constructor(private userService:UserService, private notificationService: NotificationService){}

 
  ngOnInit(){
    this.loadUserProfile();
  }
  initializeForm(user: User) {
    this.user.form.patchValue({
      username: user.username,
      password: user.password,
      firstName: user.firstName,
      lastName: user.lastName,
      email: user.email
    });
  }
  
  loadUserProfile(){
    this.userService.getUser().subscribe(
      (user) => {
        this.initializeForm(user);
        console.log(user);
      },
      (err) => {
        this.notificationService.openSnackBar('Error trying load profile.Try again.', 5000);
        console.error(err);
      }
    );
  }


  toggleEditMode(){
    this.isEditMode = !this.isEditMode;
  }
  isPasswordType(fieldName: string): string {
    return fieldName === 'password' ? 'password' : 'text';
  }
  
  saveProfile(){

    const updatedUser: UserUpdate = {
      username: this.user.form.get('username')?.value,
      password: this.user.form.get('password')?.value,
      firstName: this.user.form.get('firstName')?.value,
      lastName: this.user.form.get('lastName')?.value,
      email: this.user.form.get('email')?.value,
    };
    console.log(updatedUser);

    this.userService.updateUser(updatedUser).subscribe(
      (res) => {
        this.notificationService.openSnackBar('Updated!',3000);
        console.log(res);
        this.isEditMode = false;
      },
    (err) =>{
      this.notificationService.openSnackBar('Error trying updating, try again.',5000);
      console.error(err);
    }
    );
  }

}
