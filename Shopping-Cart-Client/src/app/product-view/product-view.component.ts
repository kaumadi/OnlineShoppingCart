import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { ProductviewService } from '../shared/services/productview.service';
import { Product } from '../shared/models/product';
import { Customer } from '../shared/models/customer';
import { AuthenticationService } from '../shared/services/authentication.service';
import { CustomerService } from '../shared/services/customer.service';
import { first } from 'rxjs/internal/operators/first';
import { Subscription } from 'rxjs/internal/Subscription';


@Component({
  selector: 'app-product-view',
  templateUrl: './product-view.component.html',
  styleUrls: ['./product-view.component.css']
})
export class ProductViewComponent implements OnInit {
  cartItems = []
  products$: Observable<Product[]>;
  totalcartvalue = 0;
  conditionToDisaply=false;
  public cartitemcount:number;
  customer_Id:number


  currentUser: Customer;
  currentUserSubscription: Subscription;
  users: Customer[] = [];
  //customers=[];
 
  constructor(private productviewService: ProductviewService,
    private customerService:CustomerService) {
      

 
  }

  ngOnInit() {
    this.loadProducts();
    this.cartitemcount=this.productviewService.getItems().length;
  // this.loadAllUsers();
   //this.currentUser.customerId=this.customer_Id
  
  }
  
  loadProducts() {
    this.products$ = this.productviewService.getAllAsync();
  }

  addToCart(product) {
    this.productviewService.addToCart(product);

    window.alert('Your product has been added to the cart!');
    this.cartitemcount=this.productviewService.getItems().length;
    console.log( this.cartitemcount);
  }

//   private loadAllUsers() {
//     this.customerService.getAllCustomers().pipe(first()).subscribe(users => {
//         this.users = users;      
//     });
// }
  
  }
 

