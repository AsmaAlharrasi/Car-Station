import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Services } from '../../datastore/services';

@Injectable({
  providedIn: 'root'
})
export class ApiCarServicesService {
  
  private apiUrl = 'https://localhost:7123/api/Service';
  

  constructor(private http:HttpClient) { }

  getAllServices(): Observable<Services[]>{
    return this.http.get<Services[]>(this.apiUrl);
  }

  getService(Id:string): Observable<Services[]>{
    return this.http.get<Services[]>(`${this.apiUrl}/${Id}`);
  }

  createService(item:Services): Observable<Services[]>{
    return this.http.post<Services[]>(this.apiUrl, item);
  }

  deleteService(Id:string): Observable<void>{
    return this.http.delete<void>(`${this.apiUrl}/${Id}`);
  }

  updateService(Id:string, item:Services): Observable<any>{
    return this.http.put<any>(`${this.apiUrl}/${Id}`, item);
  }
}
