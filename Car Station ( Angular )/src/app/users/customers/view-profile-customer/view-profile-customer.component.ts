import { Component } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { ApiCustomerService } from '../../../services/customer/api-customer.service';
import { Customer } from '../../../datastore/customer';
import { CommonModule } from '@angular/common';
import { ViewAllCarsComponent } from '../../../car/view-all-cars/view-all-cars.component';



@Component({
  selector: 'app-view-profile-customer',
  standalone: true,
  imports: [ RouterLink, CommonModule, ViewAllCarsComponent],
  templateUrl: './view-profile-customer.component.html',
  styleUrl: './view-profile-customer.component.scss'
})
export class ViewProfileCustomerComponent {
  
  customer: Customer | any;
  // customerId!:String;

constructor(private apiCustomerService: ApiCustomerService , private route: ActivatedRoute ) { }
  
  ngOnInit(): void {
    
    const customerId = String(this.route.snapshot.paramMap.get('id')); 
    this.apiCustomerService.getCustomer('3FA85F64-5717-4562-B3FC-2C963F66AFA5').subscribe(customer => {
      this.customer = customer; 
      console.log(this.customer);
    });
  }


}