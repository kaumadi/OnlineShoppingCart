import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '../shared/services/authentication.service';
import { Customer } from '../shared/models/customer';
import { Subscription } from 'rxjs';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ProductviewService } from '../shared/services/productview.service';
import { PaymentService } from '../shared/services/payment.service';
import { PaymentViewModel } from '../shared/view-models/paymentViewModel';
import { first } from 'rxjs/operators';
import { AlertService } from '../shared/services/alert.service';
import { ActivatedRoute, Router } from '@angular/router';



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
  currentDate = new Date();
  paymentViewModel:PaymentViewModel
  public paymentMethod:string
  public cartTotal: number = 0;
 



  
  constructor(
    private authenticationService: AuthenticationService,
    private formBuilder: FormBuilder,
    private productviewService: ProductviewService,
    private paymentService:PaymentService,
    private alertService: AlertService,
    private route: ActivatedRoute,
    private router: Router){
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
      contact:this.currentUser.contact,
      paymentMethod:['', [Validators.required]]


  });

  console.log(this.currentDate);
  this.items = this.productviewService.getItems();
  console.log(this.items);
  this.paymentViewModel = new PaymentViewModel();
  this.getTotalAmount();

  }


  changePaymentMethod(paymentOptione) {
    this.paymentMethod=paymentOptione.target.value
    console.log(paymentOptione.target.value);
  }

Payment(event: any){
  
  event.target.disabled = true;
  this.paymentViewModel.totalAmount=this.cartTotal
   this.paymentViewModel.orderDate=this.currentDate
   this.paymentViewModel.customerId=this.currentUser.customerId
   this.paymentViewModel.token=this.currentUser.token
   this.paymentViewModel.selectedListViewModel=this.items
   this.paymentViewModel.PaymentType=this.paymentMethod
   this.paymentService.Payment(this.paymentViewModel)
   .pipe(first())
            .subscribe(
                data => {
                  //check alert not working
                    //this.alertService.success('Payment successfully');
                    console.log(data);
                    alert('Payment successful');
                    this.clear();
                    this.router.navigate(['/orderDetails',data], { relativeTo: this.route });
                 
                   
                },
                error => {
                    this.alertService.error(error);
                                                       
                });
            
}

clear(){
  this.paymentform.reset()
  this.items=[]
  this.productviewService.items=this.items
}

getTotalAmount(){
  //this.cartTotal = 0
  this.items.forEach(item => {
    this.cartTotal += (item.orderdQty * item.unitPrice)
  })
  //this.cartTotal=this.productviewService.cartTotal
}
}

