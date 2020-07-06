import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { ProductviewService } from '../shared/services/productview.service';
import { Product } from '../shared/models/product';




@Component({
  selector: 'app-product-view',
  templateUrl: './product-view.component.html',
  styleUrls: ['./product-view.component.css']
})
export class ProductViewComponent implements OnInit {

  products$: Observable<Product[]>;
  totalcartvalue = 0;
  conditionToDisaply=false;
 
  constructor(private productviewService: ProductviewService) {
  }

  ngOnInit() {
    this.loadProducts();
  }

  loadProducts() {
    this.products$ = this.productviewService.getAllAsync();
  }

  addToCart(product) {
    this.productviewService.addToCart( product);
    window.alert('Your product has been added to the cart!');
  }
}
