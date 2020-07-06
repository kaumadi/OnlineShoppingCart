import { Component, OnInit } from '@angular/core';
import { ProductviewService } from '../shared/services/productview.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
  public cartProductCount;


  constructor(private productviewService: ProductviewService) { }

  ngOnInit(){
   
     this.cartProductCount=this.productviewService.getItems().length;
    console.log( this.cartProductCount);
  }

}
