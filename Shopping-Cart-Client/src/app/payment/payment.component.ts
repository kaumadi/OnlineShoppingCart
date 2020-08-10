import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '../shared/services/authentication.service';
import { Customer } from '../shared/models/customer';
import { Subscription } from 'rxjs';
import { FormGroup, FormBuilder } from '@angular/forms';
import { ProductviewService } from '../shared/services/productview.service';

@Component({
  selector: 'app-payment',
  templateUrl: './payment.component.html',
  styleUrls: ['./payment.component.css']
})
export class PaymentComponent implements OnInit {

  currentUser: Customer
  currentUserSubscription: Subscription
  paymentform: FormGroup;
  items
  
  constructor(
    private authenticationService: AuthenticationService,
    private formBuilder: FormBuilder,
    private productviewService: ProductviewService) {
    this.authenticationService.currentUser.subscribe(x => this.currentUser = x);
    this.currentUserSubscription = this.authenticationService.currentUser.subscribe(user => {
      this.currentUser = user;    
    });

   }

  ngOnInit() {
    this.paymentform = this.formBuilder.group({
      firstName:this.currentUser.firstName,
      lastName:this.currentUser.lastName,
      address:this.currentUser.address,
      email:this.currentUser.email,
      contact:this.currentUser.contact


  });
  this.items = this.productviewService.getItems();
  console.log(this.items);
  }
 

}
