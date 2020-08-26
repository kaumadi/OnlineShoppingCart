import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs/internal/Observable';
import { map } from 'rxjs/internal/operators/map';
import { throwError } from 'rxjs/internal/observable/throwError';
import { PaymentViewModel } from '../view-models/paymentViewModel';
import { retry } from 'rxjs/internal/operators/retry';
import { catchError } from 'rxjs/internal/operators/catchError';
import { PaymentHistoryViewModel } from '../view-models/paymentHistoryViewModel';

@Injectable({
  providedIn: 'root'
})
export class PaymentService {

  myAppUrl: string;
  myApiUrl: string;

  constructor(private http: HttpClient) {
      this.myAppUrl = environment.appUrl;
      this.myApiUrl = 'api/Order/Payment';
  
  }
  Payment(paymentViewModel:PaymentViewModel) : Observable<any>{ 
    console.log(paymentViewModel); 
    return this.http.post<any>(this.myAppUrl  + this.myApiUrl, paymentViewModel)  
    .pipe(
      map(res => res));
    } 
    
    getAllPayments(Customer_Id:number): Observable<PaymentHistoryViewModel[]> {
      return this.http.get<PaymentHistoryViewModel[]>(this.myAppUrl + 'api/Order/paymentHistory/'+Customer_Id)
      .pipe(
        retry(1),
        catchError(this.errorHandler)
      );
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
