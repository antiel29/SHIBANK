import { Component } from '@angular/core';
import { RegistrationService } from '../registration.service';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent 
{
  user: any={};
  successMessage: string = '';
  errorMessage: string = '';

  constructor(private registrationService:RegistrationService){}

  onSubmit()
  {
    this.registrationService.registerUser(this.user).subscribe(
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
