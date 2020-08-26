import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable, throwError } from 'rxjs';
import { OrderDetailsViewModel } from '../view-models/orderDetailsViewModel';
import { retry } from 'rxjs/internal/operators/retry';
import { catchError } from 'rxjs/internal/operators/catchError';
import { map } from 'rxjs/internal/operators/map';

@Injectable({
  providedIn: 'root'
})
export class OrderDetailsService {
  myAppUrl: string;
  myApiUrl: string;

  constructor(private http: HttpClient) {
      this.myAppUrl = environment.appUrl;
      this.myApiUrl = 'api/order/';
  
  }

  // getAllproductDetails(data: number): Observable<OrderDetailsViewModel> {
  //   return this.http.get<OrderDetailsViewModel>(this.myAppUrl +'api/order/${data}')
  //   .pipe(
  //     retry(1),
  //     catchError(this.errorHandler)
  //   );
   
  // }
  getAllproductDetails(id: number): Observable<any> {
    return this.http.get<any>(this.myAppUrl +`api/order/${id}`)
      .pipe(
        // map(data => {
        //   return data;
          map(res => res
        ),
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
