import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ProductViewComponent } from './product-view/product-view.component';
import { ProductCartComponent } from './product-cart/product-cart.component';
import { CustomerRegistrationComponent } from './customer-registration/customer-registration.component';
import { LoginComponent } from './login/login.component';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';


const routes: Routes = [ {
   path: '', component: ProductViewComponent, pathMatch: 'full' },
   { path: 'productCart', component: ProductCartComponent },
   { path: 'registration', component: CustomerRegistrationComponent },
   { path: 'register', component: RegisterComponent },
   { path: 'login', component: LoginComponent },
   { path: 'home', component: HomeComponent },
   { path: '**', redirectTo: '/' }];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

