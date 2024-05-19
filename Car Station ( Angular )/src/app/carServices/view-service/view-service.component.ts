import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { ApiCarServicesService } from '../../services/carServices/api-car-services.service';
import { Services } from '../../datastore/services';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';

@Component({
  selector: 'app-view-service',
  standalone: true,
  imports: [RouterLink,MatCardModule,CommonModule],
  templateUrl: './view-service.component.html',
  styleUrl: './view-service.component.scss'
})
export class ViewServiceComponent implements OnInit {
  carServices : Services | any;

  constructor(private apiCarServices : ApiCarServicesService,private route: ActivatedRoute){}

  ngOnInit(): void {
    const serviceId = String(this.route.snapshot.params['id']);

    if (serviceId) {
      console.log(serviceId);
      this.apiCarServices.getService(serviceId).subscribe(
        carServices => {
          console.log('API Response: ', carServices);
          this.carServices = carServices;
        },
        (error)=>{
            console.error('API Error: ',error);
        });
    }
  }
}
