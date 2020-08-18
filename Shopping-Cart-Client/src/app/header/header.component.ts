import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from '../shared/services/authentication.service';
import { Customer } from '../shared/models/customer';
import { CustomerService } from '../shared/services/customer.service';
import { first } from 'rxjs/internal/operators/first';
import { Subscription } from 'rxjs/internal/Subscription';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  currentUser: Customer;
  currentUserSubscription: Subscription;
  show=true 
  id:number
 

  constructor(private router: Router,
    private authenticationService: AuthenticationService) {
      this.authenticationService.currentUser.subscribe(x => this.currentUser = x);
      this.currentUserSubscription = this.authenticationService.currentUser.subscribe(user => {
        this.currentUser = user;
        if(user !==null)
        { this.show=true}
        else{
          this.show=false
        }
      });
    }

  ngOnInit(){
  
  }

  logout() {
    this.authenticationService.logout();
    this.router.navigate(['/login']);
  }

}
