import { Component } from '@angular/core';
import { LoginRequest } from '../models/login-request.model';
import { AuthService } from '../services/auth.service';
import { CookieService } from 'ngx-cookie-service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent  {
model:LoginRequest;
constructor(private authService:AuthService,private cookieService:CookieService,private router:Router){
  this.model={email:'',password:''}
}



onFormSubmit():void{
  this.authService.login(this.model)
  .subscribe({
    next:(response)=>{
      //set auth cookie
      console.log(response.Email)
      this.cookieService.set('Authorization',`Bearer ${response.Token}`,
        undefined,'/',undefined,true,'Strict'
      );
      this.authService.setUser({
        email:response.Email,
        roles:response.Roles
      })
///REDIRECT TO HOME PAGE....angular
//set user

this.router.navigateByUrl('/');
    }
  })

}
}
