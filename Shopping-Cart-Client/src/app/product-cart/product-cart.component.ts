import { Component, OnInit, EventEmitter, Output, Input} from '@angular/core';
import { ProductviewService } from '../shared/services/productview.service';
import { Product } from '../shared/models/product';
import { CheckoutService } from '../shared/services/checkout.service';
import { first } from 'rxjs/operators';
import { CheckoutViewModel } from '../shared/view-models/checkoutViewModel';
import { Customer } from '../shared/models/customer';
import { Subscription, empty } from 'rxjs';
import { AuthenticationService } from '../shared/services/authentication.service';
import { FormGroup } from '@angular/forms';
import { ProductStockStatus } from '../shared/view-models/productStockStatus';
import { SelectedListViewModel } from '../shared/view-models/SelectedListViewModel';
import { PaymentViewModel } from '../shared/view-models/paymentViewModel';
import { Router } from '@angular/router';


@Component({
  selector: 'app-product-cart',
  templateUrl: './product-cart.component.html',
  styleUrls: ['./product-cart.component.css']
})
export class ProductCartComponent implements OnInit {

  items;
  public totalAmmount;
  public productCount;
  public count:number;

  public cartTotal: number = 0;
 
  checkoutViewModel: CheckoutViewModel;
  currentUser: Customer;
  currentUserSubscription: Subscription;
  customer:Customer
  productName:string
  productStockStatus: ProductStockStatus;
  show: boolean 
  paymentViewModel:PaymentViewModel


  constructor(private productviewService: ProductviewService,
    private checoutService:CheckoutService,
    private authenticationService: AuthenticationService,private router: Router) {
      this.authenticationService.currentUser.subscribe(x => this.currentUser = x);
      this.currentUserSubscription = this.authenticationService.currentUser.subscribe(user => {
        this.currentUser = user;
        
      });
    }


  ngOnInit() {
    this.items = this.productviewService.getItems();
    console.log(this.items);
    this.getTotalAmount();
    this.productviewService.getcount();
    console.log( this.productviewService.getcount());
    this.checkoutViewModel = new CheckoutViewModel();
    this.paymentViewModel = new PaymentViewModel();
  }
 
  getTotalAmount(){
    // this.cartTotal = 0
    // this.items.forEach(item => {
    //   this.cartTotal += (item.orderdQty * item.unitPrice)
    // })
    this.cartTotal=this.productviewService.cartTotal
  }

  emptyCart() {
    this.productviewService.emptryCart();
    
  }

  removeItemFromCart(productId) {
   this.productviewService.removeProductFromCart(productId);

    //  var item=this.items.indexOf(productId);
  //  this.items.splice(item);
  this.items = this.productviewService.getItems();
  }
  

  Checkout() {
  if(this.currentUser==null){ 
    this.router.navigate(['/login']);
  }
//  this.checkoutViewModel.productId=1;
//  this.checkoutViewModel.productName="test";
//  this.checkoutViewModel.availableStockQty=1;
//  this.checkoutViewModel.unitPrice=100;
//this.items.push(this.currentUser)
else{
this.checkoutViewModel.customerId=this.currentUser.customerId
this.checkoutViewModel.userName=this.currentUser.userName
this.checkoutViewModel.token=this.currentUser.token
//this.examinationSemesters.push(this.examinationSemester);
this.checkoutViewModel.selectedListViewModel=this.items
//this.checkoutViewModel.customerId=this.currentUser.customerId;
   this.checoutService.Checkout(this.checkoutViewModel)
   .subscribe((data)=>{
     this.items=data
     this.productviewService.items=this.items
 for(var item of this.items){
if( item.productCurrentStatus==false)
{
this.show==false
}
else{
  this.show==true
}}
    console.log(this.items);
    })   
  }
}
}