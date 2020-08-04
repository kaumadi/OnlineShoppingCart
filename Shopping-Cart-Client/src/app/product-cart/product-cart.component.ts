import { Component, OnInit, EventEmitter, Output, Input} from '@angular/core';
import { ProductviewService } from '../shared/services/productview.service';
import { Product } from '../shared/models/product';
import { CheckoutService } from '../shared/services/checkout.service';
import { first } from 'rxjs/operators';
import { CheckoutViewModel } from '../shared/view-models/checkoutViewModel';


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
  cartTotal = 0
  public data: CheckoutViewModel;
  @Input() productItem: CheckoutViewModel;

  constructor(private productviewService: ProductviewService,private checoutService:CheckoutService) {

   }


  ngOnInit() {
    this.items = this.productviewService.getItems();
    console.log(this.items);
    this.getTotalAmount();
    this.productviewService.getcount();
    console.log( this.productviewService.getcount());
    
  }
 
  getTotalAmount(){
    this.totalAmmount = this.productviewService.getTotalPrice();
  }

  emptyCart() {
    this.productviewService.emptryCart();
    this.getTotalAmount()==null;
  }

  removeItemFromCart(productId) {
    this.productviewService.removeProductFromCart(productId);
    this.getTotalAmount();

  }
  
  Checkout() {
    this.checoutService.Checkout(this.items).subscribe(() => {
      console.log(this.items);
    })
  
    
  }
}