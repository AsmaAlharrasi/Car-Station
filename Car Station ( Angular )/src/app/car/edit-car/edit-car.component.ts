import { Component, OnInit } from '@angular/core';
import { ApiCarsService } from '../../services/cars/api-cars.service';
import { Car } from '../../datastore/car';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule, MatLabel } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';

@Component({
  selector: 'app-edit-car',
  standalone: true,
  imports: [
    RouterLink,
    ReactiveFormsModule,
    MatFormFieldModule,
    CommonModule,
    MatButtonModule,
    MatInputModule,
    MatLabel
  ],
  templateUrl: './edit-car.component.html',
  styleUrl: './edit-car.component.scss'
})
export class EditCarComponent implements OnInit {
  car: Car | any;
  UpdateCarForm!: FormGroup;
  Id!: string | null;



  constructor(
    private apiCarServices: ApiCarsService,
    private router: Router, private builder: FormBuilder,
    private route: ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.Id = this.route.snapshot.paramMap.get('Id') as string | null;

    this.UpdateCarForm = this.builder.group({
      customerId: ['3FA85F64-5717-4562-B3FC-2C963F66AFA1', Validators.required],
      name: ['', Validators.required],
      model: ['', Validators.required],
      vin: ['', Validators.required]
      // description: ['']


    });

    if (this.Id) {
      this.apiCarServices.getCar(this.Id).subscribe(car => {
        this.car = car;
        //Bug
        console.log(this.car);
        this.UpdateCarForm.patchValue(car);
      });
    }
  }



  onSubmit(): void {

    if (this.UpdateCarForm.valid) {
      const car = this.UpdateCarForm.value;
      car.Id = this.Id;


      this.apiCarServices.updateCar(car.Id, car).subscribe(
        () => {
          console.log(`Car with ID ${car.Id} updated successfully`)
          this.router.navigate(['ViewCustomerProfile']);
        }, (error) => {
          console.error(`Error updating Car with ID ${car.Id}:`, error);

        }
      );
    }
  }


}
