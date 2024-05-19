import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Car } from '../../datastore/car';

@Injectable({
  providedIn: 'root'
})
export class ApiCarsService {
private apiUrl = 'https://localhost:7123/api/Car';

  constructor(private http:HttpClient) {}

  getAllCars(): Observable<Car[]>{
    return this.http.get<Car[]>(this.apiUrl);
  }

  getCar(Id:string): Observable<Car[]>{
    return this.http.get<Car[]>(`${this.apiUrl}/${Id}`);
  }

  createCar(item:Car): Observable<Car[]>{
    return this.http.post<Car[]>(this.apiUrl, item);
  }

  deleteCar(Id:string): Observable<void>{
    return this.http.delete<void>(`${this.apiUrl}/${Id}`);
  }

  updateCar(Id:string, item:Car): Observable<any>{
    return this.http.put<any>(`${this.apiUrl}/${Id}`, item);
  }
}
