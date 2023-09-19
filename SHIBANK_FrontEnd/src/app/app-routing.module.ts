import { RegistrationComponent } from "./registration/registration.component";
import {NgModule} from '@angular/core'
import {RouterModule, Routes} from '@angular/router';

const routes: Routes= [ 
    { path:'register',component:RegistrationComponent },
];
@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
  })
  export class AppRoutingModule { }