import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Customer } from '../models/customer';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {

  constructor(private http: HttpClient) { }

getAllCustomers() {
    return this.http.get<Customer[]>(`${environment.appUrl}user`);
}

register(user: Customer) {
  return this.http.post(`${environment.appUrl}user/register`, user);
}
}
