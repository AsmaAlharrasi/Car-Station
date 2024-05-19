import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { ApiRequesService } from '../../../services/request/api-reques.service';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule, MatLabel } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { ApiCarServicesService } from '../../../services/carServices/api-car-services.service';
import { Services } from '../../../datastore/services';
import { ApiCarsService } from '../../../services/cars/api-cars.service';
import { Car } from '../../../datastore/car';

@Component({
  selector: 'app-customer-add-request',
  standalone: true,
  imports:
    [RouterLink,
      CommonModule,
      ReactiveFormsModule,
      MatFormFieldModule,
      MatButtonModule,
      MatInputModule,
      MatLabel,
      MatSelectModule
    ],
  templateUrl: './customer-add-request.component.html',
  styleUrl: './customer-add-request.component.scss'
})
export class CustomerAddRequestComponent implements OnInit {
  AddRequestForm: FormGroup;
  request: Request | any;
  services: any = [];
  cars: any = [];
  CustId = String(this.ActivatedRoute.snapshot.paramMap.get('CustId'));




  constructor(private apiRequestervice: ApiRequesService,
    private ApiCarService: ApiCarsService,
    private ApiCarServicesService: ApiCarServicesService,
    private builder: FormBuilder,
    private route: Router,
    private ActivatedRoute: ActivatedRoute,
    private fb: FormBuilder) {

    this.AddRequestForm = this.builder.group({
      booking: ['', Validators.required],
      carId: ['', Validators.required],
      comment: [''],
      customerId: [''],
      serviceId: ['',Validators.required],
      adminId: ['3fa85f64-5717-4562-b3fc-2c963f66afa6'],

    });
  }


  ngOnInit(): void {
    

    const allServices = this.ApiCarServicesService.getAllServices().subscribe(
      (services: Services[]) => {
        this.services = services;
      }
    );

    const allCars = this.ApiCarService.getAllCars().subscribe(
      (cars: Car[]) => {
        this.cars = cars.filter(c => c.customerId == this.CustId);
      }
    );

    
    //const admin = 

  }


  onSubmit(): void {
   
    if (this.AddRequestForm.valid) {
      const request = this.AddRequestForm.value;
      request.customerId = this.CustId;

      this.apiRequestervice.createRequest(request)
        .subscribe(
          (createItem) => {
            console.log('Request created:', createItem);
            this.route.navigate(['ViewCustomerRequest',this.CustId]);
          },
          (error) => {
            console.error('Error creating Request:', error);
          }
        );

    } else {
      console.error('Form is invalid');
    }
  }

}
