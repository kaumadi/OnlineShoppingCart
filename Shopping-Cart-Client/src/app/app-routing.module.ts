import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ProductViewComponent } from './product-view/product-view.component';
import { ProductCartComponent } from './product-cart/product-cart.component';
import { LoginComponent } from './login/login.component';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { PaymentComponent } from './payment/payment.component';


const routes: Routes = [ {
   path: '', component: ProductViewComponent, pathMatch: 'full' },
   { path: 'productCart', component: ProductCartComponent },
   { path: 'register', component: RegisterComponent },
   { path: 'login', component: LoginComponent },
   { path: 'home', component: HomeComponent },
   { path: 'payment', component: PaymentComponent },
   { path: '**', redirectTo: '/' }];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

