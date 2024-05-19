import { Component } from '@angular/core';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { Customer } from '../../../datastore/customer';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ApiCustomerService } from '../../../services/customer/api-customer.service';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule, MatLabel } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { CommonModule } from '@angular/common';


@Component({
  selector: 'app-edit-profile-customer',
  standalone: true,
  imports: [
    RouterLink,
    ReactiveFormsModule,
    MatFormFieldModule,
    CommonModule,
    MatButtonModule,
    MatInputModule,
    MatLabel,
    MatSelectModule
  ],
  templateUrl: './edit-profile-customer.component.html',
  styleUrl: './edit-profile-customer.component.scss'
})
export class EditProfileCustomerComponent {
  customer: Customer | any;
  UpdateCustomerForm!: FormGroup;
  Id!: string | null;



  constructor(
    private apiCustomerServices: ApiCustomerService,
    private router: Router, private builder: FormBuilder,
    private route: ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.Id = this.route.snapshot.paramMap.get('Id') as string | null;
    console.log(this.Id);

    this.UpdateCustomerForm = this.builder.group({
      // id: ['3fa85f64-5717-4562-b3fc-2c963f66afa1'],
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      middleName: ['', Validators.required],
      gender: ['', Validators.required],
      birthDate: [''],
      adminId: ['3fa85f64-5717-4562-b3fc-2c963f66afa6', Validators.required],
      email: ['', Validators.required],
      phoneNumber: ['', Validators.required],
      userId:['1']


      // description: ['']


    });

    if (this.Id) {
      this.apiCustomerServices.getCustomer(this.Id).subscribe(customer => {
        this.customer = customer;
        //Bug
        console.log(this.customer);
        const birthDateFormatted = this.customer.birthDate ? new Date(this.customer.birthDate).toISOString().split('T')[0] : '';
       
        this.UpdateCustomerForm.patchValue({
          firstName: this.customer.firstName,
          middleName: this.customer.middleName,
          lastName: this.customer.lastName,
          email: this.customer.email,
          gender: this.customer.gender,
          phoneNumber: this.customer.phoneNumber,
          birthDate: birthDateFormatted,
        });
        // this.UpdateCustomerForm.patchValue(customer);
      });
    }
  }



  onSubmit(): void {

    if (this.UpdateCustomerForm.valid) {
      const customer = this.UpdateCustomerForm.value;
      customer.Id = this.Id;


      this.apiCustomerServices.updateCustomer(customer.Id, customer).subscribe(
        () => {
          console.log(`Customer with ID ${customer.Id} updated successfully`)
          this.router.navigate(['ViewCustomerProfile']);
        }, (error) => {
          console.error(`Error updating Customer with ID ${customer.Id}:`, error);

        }
      );
    }
  }
}
