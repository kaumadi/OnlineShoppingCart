import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { throwError } from 'rxjs';
import { Product } from '../models/product';
import { catchError } from 'rxjs/operators';
import { retry } from 'rxjs/operators';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProductviewService {
  items = [];


  myAppUrl: string;
  myApiUrl: string;
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json; charset=utf-8'
    })
  };
  constructor(private http: HttpClient) {
      this.myAppUrl = environment.appUrl;
      this.myApiUrl = 'api/Product/';
  }

  getAllAsync(): Observable<Product[]> {
    return this.http.get<Product[]>(this.myAppUrl + this.myApiUrl)
    .pipe(
      retry(1),
      catchError(this.errorHandler)
    );
  }

  addToCart(product) {
    this.items.push(product);
    this.items.length;
  }

  getItems(){
    return this.items;
  }
  
  getcount(){
  return this.items.length;
  }


  clearCart() {
    this.items = [];
    return this.items;
  }

  
  // Calculate total price on item added to the cart
  getTotalPrice() {
    let total = 0;

    this.items.map(item => {
      total += item.unitPrice;
    });

    return total
  }

 // Remove all the items added to the cart
 emptryCart() {
  this.items.length = 0;
}

removeProductFromCart(productId) {
  this.items.map((item, index) => {
    if (item.id === productId) {
      this.items.splice(index, 1);
    }
  });
}

  errorHandler(error) {
    let errorMessage = '';
    if (error.error instanceof ErrorEvent) {
      // Get client-side error
      errorMessage = error.error.message;
    } else {
      // Get server-side error
      errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
    }
    console.log(errorMessage);
    return throwError(errorMessage);
  }
  
}
