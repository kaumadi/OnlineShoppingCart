import { BrowserModule } from '@angular/platform-browser';
import { NgModule, HostListener } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ProductViewComponent } from './product-view/product-view.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ProductviewService } from './shared/services/productview.service';
import { ProductCartComponent } from './product-cart/product-cart.component';
import { FooterComponent } from './footer/footer.component';
import { HeaderComponent } from './header/header.component';
import { Globals } from './shared/globals';
import { JwtInterceptor } from './shared/helpers/jwt.interceptor';
import { ErrorInterceptor } from './shared/helpers/error.interceptor';
import { LoginComponent } from './login/login.component';
import { AlertComponent } from './alert/alert.component';
import { RegisterComponent } from './register/register.component';
import { CheckoutService } from './shared/services/checkout.service';
import { PaymentComponent } from './payment/payment.component';
import { PaymentService } from './shared/services/payment.service';
import { OrderDetailsComponent } from './order-details/order-details.component';
import { AlertService } from './shared/services/alert.service';
import { OrderDetailsService } from './shared/services/order-details.service';
import { PaymentHistoryComponent } from './payment-history/payment-history.component';


@NgModule({
  declarations: [
    AppComponent,
    ProductViewComponent,
    ProductCartComponent,
    FooterComponent,
    HeaderComponent,
    LoginComponent,
    AlertComponent,
    RegisterComponent,
    PaymentComponent,
    OrderDetailsComponent,
    PaymentHistoryComponent,
    
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    FormsModule
  ],
  providers: [ProductviewService,Globals,CheckoutService,PaymentService,AlertService,OrderDetailsService,     
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true }],
  bootstrap: [AppComponent]
})
export class AppModule {@HostListener("window:onbeforeunload",["$event"])
clearLocalStorage(event){
    localStorage.clear() }}
