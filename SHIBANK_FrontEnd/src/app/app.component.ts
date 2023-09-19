import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms'

interface Pokemon{
  id : number,
  name: string,
  type: string,
  isCool: boolean
}

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent 
{
  title: string = "SHIBANK";
  imgSrc: string = "https://i.postimg.cc/FHRrMRvK/shiba.jpg";

  constructor()
  {
  }
}
