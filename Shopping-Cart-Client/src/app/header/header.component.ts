import { Component, OnInit } from '@angular/core';
import { ProductviewService } from '../shared/services/productview.service';
import { UserRegistrationService } from '../shared/services/user-registration.service';
import { Router } from '@angular/router';
import { AuthenticationService } from '../shared/services/authentication.service';
import { Customer } from '../shared/models/customer';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
  currentUser: Customer;
  constructor(private router: Router,
    private authenticationService: AuthenticationService) {
      this.authenticationService.currentUser.subscribe(x => this.currentUser = x);
    }

  ngOnInit(){

  }
  logout() {
    this.authenticationService.logout();
    this.router.navigate(['/login']);
}
}
