import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
registerMode = false;


  constructor(private http: HttpClient) { }

  ngOnInit() {
  }


  registerToggle() {
    this.registerMode = true;
  }


/*      Example for me
  getValues() {
    this.http.get('http://localhost:5000/api/values').subscribe(response => {
      this.values = response;
    },
    error => {
      console.log(error);
    });
  }
  */

  // this methode get data from child component
  cancelRegisterMode(registerMode: boolean) {
    this.registerMode = registerMode;
  }

}
