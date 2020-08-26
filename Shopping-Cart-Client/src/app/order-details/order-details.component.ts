import { Component, OnInit } from '@angular/core';
import { OrderDetailsService } from '../shared/services/order-details.service';
import { OrderDetailsViewModel } from '../shared/view-models/orderDetailsViewModel';
import { Observable } from 'rxjs/internal/Observable';
import { OrderItemsViewModel } from '../shared/view-models/orderItemsViewModel';
import { FormGroup, FormBuilder } from '@angular/forms';
import { THIS_EXPR } from '@angular/compiler/src/output/output_ast';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-order-details',
  templateUrl: './order-details.component.html',
  styleUrls: ['./order-details.component.css']
})
export class OrderDetailsComponent implements OnInit {

  orderDetails: OrderDetailsViewModel;
  orderDetailsForm: FormGroup;
 
  order_id: number;
  

  constructor(private orderDetailsService: OrderDetailsService,
    private formBuilder: FormBuilder,
    private router: Router,
    private activatedRouter: ActivatedRoute) { }
 
  ngOnInit(): void {
    //this.data=1013
    this.orderDetails = new OrderDetailsViewModel();
  
    this.order_id = this.activatedRouter.snapshot.params.id;
    console.log(this.order_id);
    this.loadOrderDetails(this.order_id);
    this.orderDetailsForm = this.formBuilder.group({
 
  });

  }

  loadOrderDetails(id:number) {

      this.orderDetailsService.getAllproductDetails(id).subscribe((data) => {
        this.orderDetails = data;
        console.log(this.orderDetails)
    
   })
  }
}
