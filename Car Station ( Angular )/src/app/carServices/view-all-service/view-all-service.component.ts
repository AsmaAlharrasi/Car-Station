import { Component, Input, OnInit } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { Services } from '../../datastore/services';
import { ApiCarServicesService } from '../../services/carServices/api-car-services.service';
import { MatCardModule } from '@angular/material/card';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-view-all-service',
  standalone: true,
  imports: [RouterLink,MatCardModule,CommonModule],
  templateUrl: './view-all-service.component.html',
  styleUrl: './view-all-service.component.scss'
})
export class ViewAllServiceComponent implements OnInit{
  carServices : Services[]=[];

  constructor(private apiCarServices : ApiCarServicesService, private router: Router){}

  ngOnInit(): void {
    this.apiCarServices.getAllServices().subscribe(carServices=>{ 
      console.log('API Response: ', carServices);
      this.carServices=carServices;
    },
    (error)=>{
      console.error('API Error: ',error);
    });
  }

  moreDetails(serviceId: string) {
    console.log(serviceId);
    this.router.navigate(['/ViewService',serviceId]);
  }
}
