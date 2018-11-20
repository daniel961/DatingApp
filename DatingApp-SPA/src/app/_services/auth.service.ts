import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {map} from 'rxjs/operators';
import {JwtHelperService} from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  // server url
  baseUrl = 'http://localhost:5000/api/auth/';

  // jwt helper - helps us to checks things with the token
  jwtHelper = new JwtHelperService();

  //
  decodedToken: any;

// we using the Http client to make post request
constructor(private http: HttpClient) { }

// this method take the username and password from our nav bar stored in model var
// and make post request to our server to login using them.
login(model: any) {
  return this.http.post(this.baseUrl + 'login', model)
  .pipe(
    map((response: any) => {
      const user = response;
      if (user) {
        localStorage.setItem('token', user.token);
        this.decodedToken = this.jwtHelper.decodeToken(user.token);
        // console.log(this.decodedToken);
        // console.log(user.token);
      }
    })

  );
}

register(model: any) {
  return this.http.post(this.baseUrl + 'register', model);
}

loggedIn() {
  const token = localStorage.getItem('token');
  // this return boolean if the token is (expired/no token/problem) its will be true and we want the oppsite '!'
  return !this.jwtHelper.isTokenExpired(token);
}


}
