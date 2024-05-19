
import { Component, OnInit } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { MatTableModule } from '@angular/material/table';
import { ApiCarServicesService } from '../../services/carServices/api-car-services.service';
import { Services } from '../../datastore/services';
import { CurrencyPipe } from '@angular/common';
import { CurrencyPipePipe } from '../../currency-pipe.pipe';

@Component({
  selector: 'app-admin-view-all-service',
  standalone: true,
  
  imports: [RouterLink,MatTableModule, CurrencyPipe],
  templateUrl: './admin-view-all-service.component.html',
  styleUrl: './admin-view-all-service.component.scss'
})

export class AdminViewAllServiceComponent implements OnInit {
  displayedColumns: string[] = [ 'Service Type', 'Price','Description','Action'];
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

  updateService(serviceId: string) {
    console.log(serviceId);
    this.router.navigate(['/EditService',serviceId]);
  }

  // Delete a book with confirmation alert
  deleteService(carServices:Services): void {
    console.log(carServices);

    const isConfirmed = confirm(`Are you sure you want to delete ${carServices.type} with id ${carServices.id} ?`);
    if (isConfirmed) {
      console.log(carServices.id);
      this.apiCarServices.deleteService(carServices.id).subscribe( ()=>{
        this.carServices=this.carServices.filter(c=> c.id!== carServices.id);
      });
    }
  }

}