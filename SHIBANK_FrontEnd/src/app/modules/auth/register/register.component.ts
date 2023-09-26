import { Component } from '@angular/core';
import { UserService } from 'src/app/core/services/user.service';
import { UserRegister } from 'src/app/core/models/user-register.model';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  user : UserRegister = new UserRegister ();
  successMessage: string = '';
  errorMessage: string = '';
  constructor(private userService: UserService){}

  onSubmit()
  {
    this.userService.registerUser(this.user).subscribe(
    (response) =>
    {
      this.successMessage = 'Usuario registrado exitosamente.';
      this.errorMessage = '';

    },
    (error) => 
    {
      this.successMessage = ''; 
      this.errorMessage = 'Error al registrar el usuario. Por favor, verifica los datos e intenta nuevamente.';
    }
    );
  }
}
