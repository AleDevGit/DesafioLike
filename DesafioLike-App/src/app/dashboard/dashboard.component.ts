import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  constructor(public router: Router, public authService: AuthService) { }

  ngOnInit() {
  }
  entrar(){
    this.router.navigate(['/user/login']);
  }

  loggedIn(){
    return this.authService.loggedIn();
  }

  voltarOpinar(){
    this.router.navigate(['/opinioes']);
  }

}
