import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs/internal/BehaviorSubject';
import { Observable } from 'rxjs/internal/Observable';
import { Customer } from '../models/customer';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { map } from 'rxjs/internal/operators/map';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  myAppUrl: string;
  myApiUrl: string;
    private currentUserSubject: BehaviorSubject<Customer>;
    public currentUser: Observable<Customer>;

    constructor(private http: HttpClient) {
        this.myAppUrl = environment.appUrl;
        this.myApiUrl = 'user/authenticate';
        this.currentUserSubject = new BehaviorSubject<Customer>(JSON.parse(sessionStorage.getItem('currentUser')));
        this.currentUser = this.currentUserSubject.asObservable();
    }

    public get currentUserValue(): Customer {
        return this.currentUserSubject.value;
    }

    login(username: string, password: string) {
        return this.http.post<any>(this.myAppUrl + this.myApiUrl, { username, password })
            .pipe(map(user => {
                // login successful if there's a jwt token in the response
                if (user && user.token) {
                    // store user details and jwt token in local storage to keep user logged in between page refreshes
                    sessionStorage.setItem('currentUser', JSON.stringify(user));
                    this.currentUserSubject.next(user);
                }

                
                return user;
            }));
    }

    logout() {
        // remove user from local storage to log user out
        sessionStorage.removeItem('currentUser');
        this.currentUserSubject.next(null);
    }
}

