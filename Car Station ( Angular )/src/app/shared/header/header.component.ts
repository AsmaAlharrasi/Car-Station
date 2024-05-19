import { Component, Input, OnInit } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { ApiCustomerService } from '../../services/customer/api-customer.service';
import { Customer } from '../../datastore/customer';
import { CommonModule } from '@angular/common';
import { ApiRequesService } from '../../services/request/api-reques.service';
import { Request } from '../../datastore/request';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [RouterLink,CommonModule],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent implements OnInit {
customers: Customer | any;


  constructor( private router: Router){}

  ngOnInit(): void {

  }

  moreDetails(customerId: string) {
    console.log(customerId);
    this.router.navigate(['/ViewCustomerProfile',customerId]);
  }
}
