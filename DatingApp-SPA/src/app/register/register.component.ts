import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  // send data to parent component
  @Output() cancelRegister = new EventEmitter();
  model: any = {};

  constructor(private authService: AuthService, private alertifyService: AlertifyService) { }

  ngOnInit() {
  }

  register() {
    this.authService.register(this.model).subscribe(() => {
      console.log('Register Succsess');
      this.alertifyService.success('Register Succsess!');
    }, error => {
      console.log(error);
      this.alertifyService.error('Password must be 4-8 digits and not empty');
    }

    );
  }

  cancel() {
    // send data to parent component
    this.cancelRegister.emit(false);
    console.log('Register Cancelled');
  }


}
