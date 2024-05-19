import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { MatTableModule } from '@angular/material/table';
import { Router, RouterLink } from '@angular/router';
import { ApiCustomerService } from '../../../services/customer/api-customer.service';
import { Customer } from '../../../datastore/customer';

@Component({
  selector: 'app-view-all-customer',
  standalone: true,
  imports: [RouterLink,CommonModule,MatTableModule],
  templateUrl: './view-all-customer.component.html',
  styleUrl: './view-all-customer.component.scss'
})
export class ViewAllCustomerComponent implements OnInit {
  displayedColumns: string[] = [ 'Full Name','Gender','Birth Date','Phone Number','Email','Action'];
  customerDb:Customer[]=[];
  
  constructor(private apiCustomerServices : ApiCustomerService, private router: Router){}

  ngOnInit(): void {
    this.apiCustomerServices.getAllCustomers().subscribe(customerDb=>{ 
      console.log('API Response: ', customerDb);
      this.customerDb=customerDb;
    },
    (error)=>{
      console.error('API Error: ',error);
    });
  }

  updateCustomers(customerId: string) {
    console.log(customerId);
    this.router.navigate(['/EditCustomer',customerId]);
  }

  // Delete a book with confirmation alert
  deleteCustomers(customer: Customer): void {
    console.log(customer);

    const isConfirmed = confirm(`Are you sure you want to delete ${customer.firstName} with id ${customer.id} ?`);
      if (isConfirmed) {
        this.apiCustomerServices.deleteCustomer(customer.id).subscribe(() => {
          this.customerDb = this.customerDb.filter(c => c.id !== customer.id);
        });
      }
  }
}


