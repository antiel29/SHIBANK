import { FormBuilder,FormGroup,Validators } from "@angular/forms";

export class UserRegister {
    username: string = '';
    password: string= '';
    firstname: string= '';
    lastname: string= '';
    email: string= '';


form:FormGroup;
constructor(){
    const fb = new FormBuilder();
    this.form = fb.group({
        username:['',Validators.required],
        password:['',[Validators.required,Validators.minLength(10)]],
        firstname:['',Validators.required],
        lastname:['',Validators.required],
        email:['',[Validators.required,Validators.email]],
    });
}
}
