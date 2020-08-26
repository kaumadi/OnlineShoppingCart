import { Component, OnInit } from '@angular/core';
import { PaymentService } from '../shared/services/payment.service';
import { Router, ActivatedRoute } from '@angular/router';
import { PaymentHistoryViewModel } from '../shared/view-models/paymentHistoryViewModel';

@Component({
  selector: 'app-payment-history',
  templateUrl: './payment-history.component.html',
  styleUrls: ['./payment-history.component.css']
})
export class PaymentHistoryComponent implements OnInit {

  payments: PaymentHistoryViewModel[]
  paymentHistory:PaymentHistoryViewModel
   public customer_Id:number
   orderId:number
  

  constructor(private paymentService: PaymentService,    
    private router: Router,
    private activatedRouter: ActivatedRoute) { }
 
  ngOnInit(): void {
  
    this.customer_Id = this.activatedRouter.snapshot.params.id;
    console.log(this.customer_Id);
    this.loadPaymentDetails(this.customer_Id);
    this.paymentHistory= new PaymentHistoryViewModel()

  }

  loadPaymentDetails(id:number) {
      this.paymentService.getAllPayments(id).subscribe((data) => {
        this.payments = data;
        
        console.log(this.payments)    
   })
  }



}
