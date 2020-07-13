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
  constructor(private toastr: ToastrService, public authService: AuthService, public router: Router) { }

  ngOnInit() {
  }

  nomeLogado(){
    this.name = localStorage.getItem('tokenName');
  }

  loggedIn(){
    this.nomeLogado();
    return this.authService.loggedIn();
  }
  entrar(){
    this.router.navigate(['/user/login']);
  }

  userName() {
    return sessionStorage.getItem('username');
  }

  logout(){
    localStorage.removeItem('token');
    localStorage.removeItem('tokenName');
    this.toastr.show('Log Out');
    this.router.navigate(['/dashboard']);
  }

}
