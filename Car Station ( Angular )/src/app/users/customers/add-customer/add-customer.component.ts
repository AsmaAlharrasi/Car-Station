import { Component, OnInit } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { Customer } from '../../../datastore/customer';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule, MatLabel } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { ApiCustomerService } from '../../../services/customer/api-customer.service';
import { MatSelectModule } from '@angular/material/select';
// import {Gender} from 'src/app/datastore/gender.enum';

@Component({
  selector: 'app-add-customer',
  standalone: true,
  imports: [RouterLink,
    ReactiveFormsModule,
    CommonModule,
    MatButtonModule,
    MatInputModule,
    MatSelectModule,
    MatLabel],
  templateUrl: './add-customer.component.html',
  styleUrl: './add-customer.component.scss'
})
export class AddCustomerComponent {
  AddCustomerForm!: FormGroup;
  customer: Customer | any;

  constructor(private apiCustomerServices : ApiCustomerService,
              private builder: FormBuilder,
              private route: Router) {
    
    this.AddCustomerForm = this.builder.group({
      adminId:['3fa85f64-5717-4562-b3fc-2c963f66afa6', Validators.required],
      userId:[''],
      firstName: ['', Validators.required],
      middleName: ['', Validators.required],
      lastName: ['', Validators.required],
      gender: ['', Validators.required],
      birthDate: ['', Validators.required],
      phoneNumber: ['', Validators.required],
      email: ['', Validators.required]
      // description: ['']
    });
  }

  onSubmit(): void {
    if (this.AddCustomerForm.valid) {
      const item = this.AddCustomerForm.value;
      console.log(item);
      this.apiCustomerServices.createCustomer(item)
        .subscribe(
          (createCustomer) => {
            console.log('Customer add :', createCustomer);
            this.route.navigate(['ViewAllCustomers']);
          },
          (error) => {
            console.error('Error creating Customer:', error);
          }
        );

    } else {
      console.error('Form is invalid');
    }
  }
}
