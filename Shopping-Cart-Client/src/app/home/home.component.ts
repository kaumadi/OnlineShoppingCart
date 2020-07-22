import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from '../shared/services/authentication.service';
import { Customer } from '../shared/models/customer';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  currentUser: Customer;

  constructor(private router: Router,
    private authenticationService: AuthenticationService) {  this.authenticationService.currentUser.subscribe(x => this.currentUser = x);}

  ngOnInit() {
  }
  logout() {
    this.authenticationService.logout();
    this.router.navigate(['/login']);
}
}
