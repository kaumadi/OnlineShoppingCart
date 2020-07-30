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
  private currentUserSubject: BehaviorSubject<Customer>;
  public currentUser: Observable<Customer>;

  constructor(private router: Router,private http: HttpClient) {
      this.currentUserSubject = new BehaviorSubject<Customer>(JSON.parse(localStorage.getItem('currentUser')));
      this.currentUser = this.currentUserSubject.asObservable();
  }

  public get currentUserValue(): Customer {
      return this.currentUserSubject.value;
  }

  login(username: string, password: string) {
      return this.http.post<any>(`${environment.appUrl}user/authenticate`, { username, password })
          .pipe(map(user => {
              // store user details and jwt token in local storage to keep user logged in between page refreshes
              localStorage.setItem('currentUser', JSON.stringify(user));
              this.currentUserSubject.next(user);
              return user;
          }));
  }

  logout() {
      // remove user from local storage to log user out
      localStorage.removeItem('currentUser');
      this.currentUserSubject.next(null);
      this.router.navigate(['/account/login']);
  }

register(user: Customer) {
    return this.http.post(`${environment.appUrl}user/register`, user);
}

getAll() {
    return this.http.get<Customer[]>(`${environment.appUrl}user`);
}

getById(id: string) {
    return this.http.get<Customer>(`${environment.appUrl}user/${id}`);
}

}

