import { Employee } from './../../datastore/employee';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class ApiEployeeService {
  private apiUrl = 'https://localhost:7123/api/Employee';


  constructor(private http: HttpClient) { }

  getEmployees(): Observable<Employee[]> {
    return this.http.get<Employee[]>(this.apiUrl)
  }

  getEmployee(employeeId: String): Observable<Employee> {
    return this.http.get<Employee>(`${this.apiUrl}/${employeeId}`);
  }

  addEmployee(employee : Employee) : Observable<Employee> {
    return this.http.post<Employee>(`${this.apiUrl}`, employee);
  }

  updateEmployee(employeeID:String, employee: Employee): Observable<Employee> {
    const url = `${this.apiUrl}/${employeeID}`; 
    return this.http.put<Employee>(url, employee); 
  }

  deleteEmployee(employeeId : String): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${employeeId}`);
  }


}
