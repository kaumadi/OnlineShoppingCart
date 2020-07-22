import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
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
import { CustomerRegistrationComponent } from './customer-registration/customer-registration.component';
import { JwtInterceptor } from './shared/helpers/jwt.interceptor';
import { ErrorInterceptor } from './shared/helpers/error.interceptor';
import { LoginComponent } from './login/login.component';
import { HomeComponent } from './home/home.component';

@NgModule({
  declarations: [
    AppComponent,
    ProductViewComponent,
    ProductCartComponent,
    FooterComponent,
    HeaderComponent,
    CustomerRegistrationComponent,
    LoginComponent,
    HomeComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    FormsModule
  ],
  providers: [ProductviewService,Globals,        
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true }],
  bootstrap: [AppComponent]
})
export class AppModule { }
