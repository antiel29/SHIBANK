import { FormBuilder,FormGroup,Validators } from "@angular/forms";

export class UserLogin 
{
    username: string = '';
    password: string= '';
    form: FormGroup;

    constructor()
    {
        const fb = new FormBuilder();

        this.form = fb.group
        ({
            username:['',Validators.required],
            password:['',Validators.required],
        });
    }
    
}