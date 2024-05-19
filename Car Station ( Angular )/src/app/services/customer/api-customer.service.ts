import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Customer } from '../../datastore/customer';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ApiCustomerService {

  private apiUrl = 'https://localhost:7123/api/Customer';

  constructor(private http:HttpClient) {}

  getAllCustomers(): Observable<Customer[]>{
    return this.http.get<Customer[]>(this.apiUrl);
  }

  getCustomer(Id:string): Observable<Customer[]>{
    return this.http.get<Customer[]>(`${this.apiUrl}/${Id}`);
  }

  createCustomer(item:Customer): Observable<Customer[]>{
    return this.http.post<Customer[]>(this.apiUrl, item);
  }

  deleteCustomer(Id:string): Observable<void>{
    return this.http.delete<void>(`${this.apiUrl}/${Id}`);
  }

  updateCustomer(Id:string, item:Customer): Observable<any>{
    return this.http.put<any>(`${this.apiUrl}/${Id}`, item);
  }
}
