import {Component, OnInit} from '@angular/core';
import {PrimeNGConfig} from "primeng/api";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {Router} from "@angular/router";
import {AuthService} from "../../../service/auth/auth.service";


@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

  constructor(private primengConfig: PrimeNGConfig,
              private route: Router,
              private authService: AuthService) { }

  ngOnInit() {
    this.primengConfig.ripple = true;
  }
  registerGroup = new FormGroup({
    Username: new FormControl("", Validators.required),
    Password: new FormControl("", Validators.required),
    Email: new FormControl("", Validators.required),
    Firstname: new FormControl("", Validators.required),
    Surname: new FormControl(""),
  });

  RegisterUser() {
    this.authService.registerUser(this.registerGroup.value)
      .subscribe({
        next: (response: any) => {
          console.log(response)
        },
        complete: () => {
          this.route.navigate(['/auth/login'])
        },
        error: (err) => {
          console.log(err)
        }
      })

  }


}
