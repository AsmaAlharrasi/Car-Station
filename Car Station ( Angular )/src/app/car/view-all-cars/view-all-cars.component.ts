import { Component, Input, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { Car } from '../../datastore/car';
import { ApiCarsService } from '../../services/cars/api-cars.service';
import { CommonModule, LowerCasePipe, UpperCasePipe } from '@angular/common';

import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';

@Component({
  selector: 'app-view-all-cars',
  standalone: true,
  imports: [
    RouterLink,
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatCardModule
  ],
  templateUrl: './view-all-cars.component.html',
  styleUrl: './view-all-cars.component.scss'
})
export class ViewAllCarsComponent implements OnInit {

  @Input() selectedCustomerId: string ='';

  columnsToDisplay: string[] = ['Name', 'Model', 'VIN', 'edit', 'delete'];
  car: Car[] = [];

  constructor(private apiCarService: ApiCarsService) { }

  ngOnInit(): void {
   
    this.apiCarService.getAllCars().subscribe(
      (car: Car[]) => {
        this.car = car.filter(c => c.customerId == this.selectedCustomerId);

        if (car.length > 0) {
          console.log(car);
        } else {
          console.log("car not found");
        }

      });
  }

  // Delete a car with confirmation alert
  deleteCar(car: Car): void {
    console.log(car);

    const isConfirmed = confirm(`Are you sure you want to delete ${car.name} with id ${car.id} ?`);
    if (isConfirmed) {
      this.apiCarService.deleteCar(car.id).subscribe(() => {
        this.car = this.car.filter(c => c.id !== car.id);
      });
    }
  }

}
