import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { CheckoutViewModel } from '../view-models/checkoutViewModel';
import { Observable, throwError } from 'rxjs';
import { map } from 'rxjs/internal/operators/map';

@Injectable({
  providedIn: 'root'
})
export class CheckoutService {


  myAppUrl: string;
  myApiUrl: string;

  constructor(private http: HttpClient) {
      this.myAppUrl = environment.appUrl;
      this.myApiUrl = 'api/order/Checkout';
  
  }
  Checkout(checkoutViewModel:CheckoutViewModel) : Observable<any>{  
    return this.http.post<any>(this.myAppUrl  + this.myApiUrl, checkoutViewModel)  
      .pipe(
        map((response: Response) => 
        response.json()));
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
