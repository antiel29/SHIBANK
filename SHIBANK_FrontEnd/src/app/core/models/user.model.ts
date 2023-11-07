import { FormBuilder,FormGroup,Validators } from "@angular/forms";

export class User  
{
    username: string = '';
    password: string= '';
    firstName: string= '';
    lastName: string= '';
    email: string= '';
    formFields: 
    { id: keyof User, label: string }[] = 
    [
    { id: 'username', label: 'Username' },
    { id: 'password', label: 'Password' },
    { id: 'firstName', label: 'First Name' },
    { id: 'lastName', label: 'Last Name' },
    { id: 'email', label: 'Email' },
    ];
    form:FormGroup;
    
    constructor()
    {
    const fb = new FormBuilder();

    this.form = fb.group
        ({
            username:['',Validators.required],
            password:['',[Validators.required,Validators.minLength(10)]],
            firstName:['',Validators.required],
            lastName:['',Validators.required],
            email:['',[Validators.required,Validators.email]],
        });
    }
}