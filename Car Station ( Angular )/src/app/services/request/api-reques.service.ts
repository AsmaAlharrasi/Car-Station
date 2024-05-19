import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Request } from '../../datastore/request';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ApiRequesService {

  private apiUrl = 'https://localhost:7123/api/ServicesRequest';

  constructor(private http:HttpClient) {}

  getAllRequests(): Observable<Request[]>{
    return this.http.get<Request[]>(this.apiUrl);
  }

  getRequest(Id:string): Observable<Request[]>{
    return this.http.get<Request[]>(`${this.apiUrl}/${Id}`);
  }

  createRequest(item:Request): Observable<Request[]>{
    return this.http.post<Request[]>(this.apiUrl, item);
  }

  deleteRequest(Id:string): Observable<void>{
    return this.http.delete<void>(`${this.apiUrl}/${Id}`);
  }

  updateRequest(Id:string, item:Request): Observable<any>{
    return this.http.put<any>(`${this.apiUrl}/${Id}`, item);
  }
}
