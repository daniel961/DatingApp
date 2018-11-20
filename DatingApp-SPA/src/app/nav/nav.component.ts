import { Component, OnInit } from '@angular/core';
import { ExtraLocaleDataIndex } from '@angular/common/src/i18n/locale_data';
import {AuthService} from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  // this var will save our username and password
  model: any = {};


  // inject auth service to our nav component
  constructor(public authService: AuthService, private alertifyService: AlertifyService) { }

  ngOnInit() {
  }

  login() {
    // with this login method we call AuthService login method and print in consol the result
    this.authService.login(this.model).subscribe(
         next => {
        this.alertifyService.success('Logged in Succsessfully');
        console.log('Logged in Succsessfully');
      }, error => {
        this.alertifyService.error('Login Faild Username or Password incorrect');
        console.log('Faild Log in :( ');
      }
    );
  }

  loggedIn() {
    // here we check if the token is expired or not
    return this.authService.loggedIn();
  }

  logout() {
    // remove the token content in our local storge for logging out
    localStorage.removeItem('token');
    console.log('Logged Out');
    this.alertifyService.warning('Logged Out');

  }

}
