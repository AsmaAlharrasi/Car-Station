import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { ApiRequesService } from '../../../services/request/api-reques.service';
import { Request } from '../../../datastore/request';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { ApiCarServicesService } from '../../../services/carServices/api-car-services.service';
import { ApiCarsService } from '../../../services/cars/api-cars.service';

@Component({
  selector: 'app-customer-view-all-request',
  standalone: true,
  imports: [
    RouterLink,
    CommonModule,
    MatTableModule,
    MatButtonModule
  ],
  templateUrl: './customer-view-all-request.component.html',
  styleUrl: './customer-view-all-request.component.scss'
})
export class CustomerViewAllRequestComponent implements OnInit {

  columnsToDisplay: string[] = ['ServicesType', 'VIN', 'Date', 'Comment', 'Status', 'Action'];
  requests: Request[] = [];

  CustId = String(this.ActivatedRoute.snapshot.paramMap.get('CustId'));


  serviceNames: Map<string, string> = new Map();
  carNames: Map<string, string> = new Map();


  constructor(
    private apiRequestService: ApiRequesService,
    private apiCarServiceServices: ApiCarServicesService,
    private apiCarService: ApiCarsService,
    private ActivatedRoute: ActivatedRoute,
  ) { }

  ngOnInit(): void {

    this.loadTheData();

    // this.apiRequestService.getAllRequests().subscribe(

    //   (data: Request[]) => {

    //     this.requests = data;

    //     if (data.length > 0) {
    //       console.log(data);
    //     } else {
    //       console.log("Request not found");
    //     }

    //   }
    // );
    const allRequests = this.apiRequestService.getAllRequests().subscribe(
      (request: Request[]) => {
        this.requests = request.filter(c => c.customerId == this.CustId);
      }
    );

  }

  loadTheData() {

    this.apiRequestService.getAllRequests().subscribe(request => {
      // this.requests = request.filter(request => request.status != 'Canceled');
      console.log(request, "requesttt");
      this.requests = request;
      request.forEach(request => {
        this.fetchServiceName(request.serviceId);
        this.fetchCarName(request.carId);

      });
    },
      (error) => {
        console.error('API Error:', error);

      });

  }

  fetchServiceName(serviceID: string) {
    this.apiCarServiceServices.getAllServices().subscribe(
      carServices => {
        if (carServices) {
          const foundService = carServices.find(service => service.id === serviceID);

          // console.log(foundService,'service')
          if (foundService) {
            this.serviceNames.set(serviceID, foundService.type);
          } else {
            console.log('Service with ID not found:', serviceID);
          }

        } else {
          console.error('Service not found for ID:', serviceID);
        }
      }, error => {
        console.error('API Error : ', error);
      }
    );
  }

  fetchCarName(carID: string) {
    this.apiCarService.getAllCars().subscribe(
      Car => {
        if (Car) {
          // console.log(Car);
          const foundService = Car.find(Car => Car.id === carID);
          if (foundService) {
            this.carNames.set(carID, foundService.vin);
            // console.log('Car ID:', carID);
          } else {
            console.log('Car with ID not found:', carID);
          }
        } else {
          console.error('Car not found for ID:', carID);
        }
      },
      error => {
        console.error('API Error : ', error);
      }
    );
  }


  cancelRequest(serviceRequestId: string) {

    console.log('id', serviceRequestId);
    if (serviceRequestId) {
      this.apiRequestService.getAllRequests().subscribe(
        serviceReuest => {
          if (serviceReuest) {
            console.log('full object', serviceReuest);
            const foundServiceRequest = serviceReuest.find(Request => Request.id === serviceRequestId);
            console.log('id match', foundServiceRequest);
            if (foundServiceRequest) {
              const update = {
                id: foundServiceRequest.id,
                status: '3', //'3' represents the status for 'Cancelled'
                date: foundServiceRequest.date,
                //  location:foundServiceRequest.location,
                time: foundServiceRequest.time,
                comment: foundServiceRequest.comment,
                serviceId: foundServiceRequest.serviceId,
                customerId: foundServiceRequest.customerId,
                employeeId: foundServiceRequest.employeeId,
                carId: foundServiceRequest.carId,
                adminId: foundServiceRequest.adminId
              }
              this.apiRequestService.updateRequest(serviceRequestId, update).subscribe(
                () => {
                  console.log('status updated , ', foundServiceRequest);
                  console.log('new status : ', foundServiceRequest.status);
                  this.loadTheData();
                }
              )
            }
          }
        }
      );
    }

  }


}
