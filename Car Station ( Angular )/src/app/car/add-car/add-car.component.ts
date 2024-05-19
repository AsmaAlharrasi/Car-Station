import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink, ActivatedRoute } from '@angular/router';
import { Car } from '../../datastore/car';
import { ApiCarsService } from '../../services/cars/api-cars.service';
import { MatFormFieldModule, MatLabel } from '@angular/material/form-field';

import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';

@Component({
  selector: 'app-add-car',
  standalone: true,
  imports:
  [
    RouterLink,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatButtonModule,
    MatInputModule,
    MatLabel

  ],
  templateUrl: './add-car.component.html',
  styleUrl: './add-car.component.scss'
})
export class AddCarComponent {
  AddCarForm!: FormGroup;
  car: Car | any;

  constructor(private apiCarservice: ApiCarsService, private builder: FormBuilder, private route: Router, private ActivatedRoute:ActivatedRoute) {
    const customerId = String(this.ActivatedRoute.snapshot.paramMap.get('CustId')); 


    this.AddCarForm = this.builder.group({
      customerId:[customerId],
      name: ['', Validators.required],
      model: ['', Validators.required],
      vin: ['', Validators.required]
      // description: ['']
    });
  }


  onSubmit(): void {
    if (this.AddCarForm.valid) {
      const item = this.AddCarForm.value;

      this.apiCarservice.createCar(item)
        .subscribe(
          (createItem) => {
            console.log('Car created:', createItem);
            this.route.navigate(['ViewCustomerProfile']);
          },
          (error) => {
            console.error('Error creating Car:', error);
          }
        );

    } else {
      console.error('Form is invalid');
    }
  }


}


