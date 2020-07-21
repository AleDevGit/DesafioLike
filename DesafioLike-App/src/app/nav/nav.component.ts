import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { User } from '../_models/User';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  name = '';
  _admin = false;
  _operador = false;
  constructor(private toastr: ToastrService, public authService: AuthService, public router: Router) { }

  ngOnInit() {
    this.userName();
  }
  isAdmin(){
    return this._admin;
  }
  isOperador(){
    if (this._admin){
      this.buscarRegras();
      return this._operador = this._admin;
    }else{
      this.buscarRegras();
       return this._operador;
    }
  }
  showMenu() {
    return this.router.url !== '/dashboard';
  }

  nomeLogado(){
    this.name = localStorage.getItem('tokenName');
  }

  loggedIn(){
    console.log(this.authService.loggedIn());
    return this.authService.loggedIn();
  }

  buscarRegras(){
    if (localStorage.getItem('token') !== null){
      const jwt = localStorage.getItem('token');
      const jwtData = jwt.split('.')[1];
      const decodedJwtJsonData = window.atob(jwtData);
      const decodedJwtData = JSON.parse(decodedJwtJsonData);
      const roles = decodedJwtData.role;
      this._admin = false;
      this._operador = false;
      if (roles !== undefined){
        if (roles.includes('Admin')){
          this._admin = true;
          this._operador = true;
        }else if (roles.includes('Operador')){
          this._operador = true;
        }
      }
    }else{
      this._admin = false;
      this._operador = false;
    }
  }

  userName() {
    if (sessionStorage.getItem('username') === null){
      localStorage.removeItem('token');
      this.router.navigate(['/dashboard']);
    }
    return sessionStorage.getItem('username');
  }

  logout(){
    localStorage.removeItem('token');
    sessionStorage.removeItem('username');
    this.router.navigate(['/dashboard']);
  }

}
