import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ReactiveFormsModule, FormBuilder, Validators, FormGroup } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule, MatLabel } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { RouterLink, Router, ActivatedRoute } from '@angular/router';
// import { Customer } from '../../../datastore/customer';
import { ApiCustomerService } from '../../../services/customer/api-customer.service';
import { MatSelectModule } from '@angular/material/select';
import { Customer } from '../../../datastore/customer';


@Component({
  selector: 'app-admin-edit-customer',
  standalone: true,
  imports: [RouterLink,
            ReactiveFormsModule,
            MatFormFieldModule,
            CommonModule,
            MatButtonModule,
            MatInputModule,
            MatSelectModule,
            MatLabel],
  templateUrl: './admin-edit-customer.component.html',
  styleUrl: './admin-edit-customer.component.scss'
})
export class AdminEditCustomerComponent implements OnInit {
  editCustomerForm: FormGroup;
  customer: Customer | any;
  // Id!: string | null;

  constructor(private apiCustomerServices : ApiCustomerService,
              private builder: FormBuilder, 
              private router: Router,
              private route: ActivatedRoute) {

                this.editCustomerForm=this.builder.group({
                  adminId:['3fa85f64-5717-4562-b3fc-2c963f66afa6', Validators.required],
                  userId:[''],
                  firstName: ['', Validators.required],
                  middleName: ['', Validators.required],
                  lastName: ['', Validators.required],
                  gender: ['', Validators.required],
                  birthDate: ['', Validators.required],
                  phoneNumber: ['', Validators.required],
                  email: ['', Validators.required]
                });
              }

    
  ngOnInit(): void {
    const cusID = String(this.route.snapshot.paramMap.get('Id'));

    this.apiCustomerServices.getCustomer(cusID).subscribe(
      (customer) => {
        this.customer = customer;
        console.log(this.customer); // Do something with the retrieved book
        const birthDateFormatted = this.customer.birthDate ? new Date(this.customer.birthDate).toISOString().split('T')[0] : '';

        this.editCustomerForm.patchValue({
                  // customerId:this.customer.Id,
                  // adminId:['3fa85f64-5717-4562-b3fc-2c963f66afa6', Validators.required],
                  // userId:this.customer.userId,
                  firstName:this.customer.firstName,
                  middleName:this.customer.middleName,
                  lastName:this.customer.lastName,
                  gender:this.customer.gender,
                  birthDate:birthDateFormatted,
                  phoneNumber:this.customer.phoneNumber,
                  email:this.customer.email
                });
      }
    );

  }

  onSubmit(){
    if (this.editCustomerForm.valid) {
      const Newcustomer = this.editCustomerForm.value;
      Newcustomer.Id = this.customer.id;
      console.log(Newcustomer.id);
      this.apiCustomerServices.updateCustomer(Newcustomer.Id,Newcustomer).subscribe(
        () => {
          console.log(`Customer with ID ${Newcustomer.Id} updated successfully`)
          this.router.navigate(['ViewAllCustomers']);
        }, (error) => {
          console.error(`Error updating customer with ID ${Newcustomer.Id}:`, error);

        }
      );
    }
  }



  }


