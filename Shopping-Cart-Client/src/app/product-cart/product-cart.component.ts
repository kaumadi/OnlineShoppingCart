import { Component, OnInit } from '@angular/core';
import { ProductviewService } from '../shared/services/productview.service';


@Component({
  selector: 'app-product-cart',
  templateUrl: './product-cart.component.html',
  styleUrls: ['./product-cart.component.css']
})
export class ProductCartComponent implements OnInit {
  items;
  public totalAmmount;

  constructor(private productviewService: ProductviewService) { }

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
}
