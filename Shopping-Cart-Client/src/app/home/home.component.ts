import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from '../shared/services/authentication.service';
import { Customer } from '../shared/models/customer';
import { Subscription } from 'rxjs/internal/Subscription';
import { CustomerService } from '../shared/services/customer.service';
import { first } from 'rxjs/internal/operators/first';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
 
currentUser: Customer;
currentUserSubscription: Subscription;
users: Customer[] = [];

constructor(
    private authenticationService: AuthenticationService,
    private userService: CustomerService
) {
    this.currentUserSubscription = this.authenticationService.currentUser.subscribe(user => {
        this.currentUser = user;
    });
}

ngOnInit() {
    this.loadAllUsers();
}

ngOnDestroy() {
    // unsubscribe to ensure no memory leaks
    this.currentUserSubscription.unsubscribe();
}



private loadAllUsers() {
    this.userService.getAllCustomers().pipe(first()).subscribe(users => {
        this.users = users;
    });
}
}
