import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { LoginService } from "./login.service";
import { User } from "../model/user";
import { HttpHelpersService } from "../service/http-helpers.service";

@Component({
    templateUrl: 'login.component.html',
    moduleId: module.id
    
})

export class LoginComponent implements OnInit {
    //currentUser: User = new User();
    loading = false;
    error = '';
    user:User;//:any = { email:'', password:''}
    mensaje = "";
    constructor(
        private router: Router,
        private httpService : HttpHelpersService,
        private loginService:LoginService) { }

    ngOnInit() {
     

        this.user=new  User();
        this.user.userName="moviedo";
        this.user.password="1234";
    }

    login() {
        
        this.mensaje="validando...";
        this.loginService.logIn(this.user)
            .subscribe(
                result=>{
                    //console.log(result);
                }, 
                e=>{
                    this.error = e;
                });
        // this.authenticationService.login(this.currentUser.userName, this.currentUser.Password)
        //     .subscribe(result => {
        //         if (result === true) {
        //             this.router.navigate(['/']);
        //         } else {
        //             this.error = 'userName or password is incorrect';
        //             this.loading = false;
        //         }
        //     });

       
    }
    registry(){
        console.log('Enviando credenciales para registrar: ' + JSON.stringify(this.user));
        this.mensaje="validando...";
        this.loginService
            .registry(this.user)
            .subscribe(
                result=>{
                    console.log(result);
                }, 
                e=>{
                    this.mostrarError(e);
                })
    }

    mostrarError(e){
        this.mensaje="ERROR";
        console.error(e);
    }

}
