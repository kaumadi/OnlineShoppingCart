import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
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

// Override JSON.parse for debug purposes
(function () {
  var parse = JSON.parse;

  JSON.parse = function (str) {
      try {
          return parse.apply(this, arguments);
      } catch (e) {
          console.log('Error parsing', arguments);
          throw e;
      }
  }
}());


// Override XMLHttpRequest.open
(function() {
  var origOpen = XMLHttpRequest.prototype.open;
  XMLHttpRequest.prototype.open = function() {
      this.addEventListener('load', function() {
          console.log('Http Response', this.responseText, this);
      });
      origOpen.apply(this, arguments);
  };
})();


@NgModule({
  declarations: [
    AppComponent,
    ProductViewComponent,
    ProductCartComponent,
    FooterComponent,
    HeaderComponent,
    CustomerRegistrationComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    FormsModule
  ],
  providers: [ProductviewService,Globals],
  bootstrap: [AppComponent]
})
export class AppModule { }
