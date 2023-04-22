import { Component } from '@angular/core';
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {MessageService} from "primeng/api";
import {Router} from "@angular/router";
import {AuthService} from "../../../service/auth/auth.service";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
  providers: [MessageService]
})
export class LoginComponent {

  constructor(private route: Router, private authService: AuthService) {
  }


  loginGroup = new FormGroup({
    email: new FormControl("", Validators.required),
    password: new FormControl("", Validators.required)
  });


  Login() {
    if (this.loginGroup.valid) {
      //console.log('form valid oldu');
      //this.route.navigate(['/passwords/mypasswords']);
      console.log(this.loginGroup.value)
      this.authService.postLogin(this.loginGroup.value)
        .subscribe({
          next: (response: any) => {
            console.log(response)
          },
          complete: () => {
            console.log("complete")
            this.route.navigate(['/passwords/mypasswords'])
          },
          error: (err) => {
            console.log(err)
          }
        })
    }
  }

}
