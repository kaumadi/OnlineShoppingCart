import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ProductViewComponent } from './product-view/product-view.component';
import { ProductCartComponent } from './product-cart/product-cart.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { PaymentComponent } from './payment/payment.component';
import { OrderDetailsComponent } from './order-details/order-details.component';
import { PaymentHistoryComponent } from './payment-history/payment-history.component';


const routes: Routes = [ {
   path: '', component: ProductViewComponent, pathMatch: 'full' },
   { path: 'productCart', component: ProductCartComponent },
   { path: 'register', component: RegisterComponent },
   { path: 'login', component: LoginComponent },
   { path: 'payment', component: PaymentComponent },
   { path: 'orderDetails/:id', component: OrderDetailsComponent },
   { path: 'paymentHistory/:id', component: PaymentHistoryComponent },
   { path: '**', redirectTo: '/' }];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

