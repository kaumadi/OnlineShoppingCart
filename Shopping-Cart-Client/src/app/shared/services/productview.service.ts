import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { throwError } from 'rxjs';
import { Product } from '../models/product';
import { catchError } from 'rxjs/operators';
import { retry } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { Globals } from '../globals';

@Injectable({
  providedIn: 'root'
})
export class ProductviewService {
  items = [];
  public cartcount:number;
  formData: Product;
  public count:number;
  public productName:string

  
  myAppUrl: string;
  myApiUrl: string;
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json; charset=utf-8'
    })
  };
  constructor(private http: HttpClient,public globals:Globals) {
      this.myAppUrl = environment.appUrl;
      this.myApiUrl = 'api/Product/';
       this.cartcount=globals.count;
  }

  getAllAsync(): Observable<Product[]> {
    return this.http.get<Product[]>(this.myAppUrl + this.myApiUrl)
    .pipe(
      retry(1),
      catchError(this.errorHandler)
    );
  }

 //addToCart(product) {
  //  const existing = this.items.find(({name}) => product.ProductName === name);
  //     if (existing) {
  //       existing.num +=1;
  //       return;
  //     }
  //    this.items.push({...product, num: 1});
  // const productExistInCart = this.items.find(({id}) => id === product.productId); // find product by name
 // if (productExistInCart) {
    
  //   this.items.push({...product, count:1+1}); // enhance "porduct" opject with "num" property
 
  //  return;
 // }
  //productExistInCart.count += 1;
 //  this.items.push(product);
 //}
 public cartTotal = 0

  addToCart(product) {

    let productExists = false

    for (let i in this.items) {
      if (this.items[i].productId === product.productId) {
        this.items[i].orderdQty++
        productExists = true
        break;
      }
    }

    if (!productExists) {
      this.items.push({
        productId: product.productId,
        productName: product.productName,
        orderdQty: 1,
        unitPrice: product.unitPrice,
        productCurrentStatus:true
       
      })
    }

    this.cartTotal = 0
    this.items.forEach(item => {
      this.cartTotal += (item.orderdQty * item.unitPrice)
    })
  }
  getItems(){
    return this.items;
  }
  
  getcount(){
  return this.items.length;

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
    this.cartTotal=null
  }
 
  removeProductFromCart(productId) {
    var item=this.items.indexOf(productId);  
    this.items.splice(item);

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
