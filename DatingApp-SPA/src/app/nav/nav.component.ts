import { Component, OnInit } from '@angular/core';
import { ExtraLocaleDataIndex } from '@angular/common/src/i18n/locale_data';
import {AuthService} from '../_services/auth.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  // this var will save our username and password
  model: any = {};


  // inject auth service to our nav component
  constructor(private authService: AuthService) { }

  ngOnInit() {
  }

  login() {
    // with this login method we call AuthService login method and print in consol the result
    this.authService.login(this.model).subscribe(
         next => {
        console.log('Logged in Succsessfully');
      }, error => {
        console.log('Faild Log in :( ');
      }
    );
  }

  loggedIn() {
    const token = localStorage.getItem('token');
    return !!token;
  }

  logout() {
    // remove the token content in our local storge for logging out
    localStorage.removeItem('token');
    console.log('Logged Out');

  }

}
