import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs/internal/Observable';
import { throwError } from 'rxjs/internal/observable/throwError';
import { map } from 'rxjs/internal/operators/map';
import { catchError, retry } from 'rxjs/operators';
import {RegistrationViewModel} from '../view-models/RegistrationViewModel';


@Injectable({
  providedIn: 'root'
})
export class UserRegistrationService {


  myAppUrl: string;
  myApiUrl: string;
  // httpOptions = {
  //   headers: new HttpHeaders({
  //     'Content-Type': 'application/json',
      
  //   })
  // };
  constructor(private http: HttpClient) {
      this.myAppUrl = environment.appUrl;
      this.myApiUrl = 'api/Accounts/';
  
  }
  RegisterCustomer(employee:RegistrationViewModel) : Observable<any>{  
    return this.http.post<any>(this.myAppUrl  + this.myApiUrl, employee)  
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
