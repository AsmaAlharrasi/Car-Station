import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { MatTableModule } from '@angular/material/table';
import { Router, RouterLink } from '@angular/router';
import { ApiRequesService } from '../../../../services/request/api-reques.service';
import { ApiCarServicesService } from '../../../../services/carServices/api-car-services.service';
import { ApiCustomerService } from '../../../../services/customer/api-customer.service';
import { ApiCarsService } from '../../../../services/cars/api-cars.service';
import { Request } from '../../../../datastore/request';

@Component({
  selector: 'app-new-request',
  standalone: true,
  imports: [RouterLink,CommonModule,MatTableModule],
  templateUrl: './new-request.component.html',
  styleUrl: './new-request.component.scss'
})
export class NewRequestComponent implements OnInit {
  displayedColumns: string[] = ['customerName','serviceType','date','carVIN', 'comment','status','employeeId','Action'];
  sRequest :Request[]=[];

  serviceNames: Map<string, string> = new Map();
  customerNames: Map<string, string> = new Map();
  carNames: Map<string, string> = new Map();



  constructor(private apiRequestService: ApiRequesService,
              private apiCarServices : ApiCarServicesService,
              private apiCustomerServices : ApiCustomerService,
              private apiCarService: ApiCarsService,
              private router: Router){}

  ngOnInit(): void {

    this.loadTheData();
    
    
  }

  //display all data
  loadTheData(){
    this.apiRequestService.getAllRequests().subscribe(sRequest=>{
      this.sRequest = sRequest.filter(sRequest => sRequest.employeeId == null && sRequest.status != 'Canceled' );
      //console.log('Filtered requests:', this.sRequest);
      // this.sRequest=sRequest;
      sRequest.forEach(sRequest =>{
        this.fetchServiceName(sRequest.serviceId);
        this.fetchCustomerName(sRequest.customerId);
        this.fetchCarName(sRequest.carId);

      });
    },
    (error)=>{
      console.error('API Error: ',error);
    });
  }
  //Getting the service Type 
  fetchServiceName(serviceID: string) {
    this.apiCarServices.getAllServices().subscribe(
      carServices => {
        if (carServices) {
          // console.log(carServices);
          const foundService = carServices.find(service => service.id === serviceID);
          if (foundService) {
            this.serviceNames.set(serviceID, foundService.type);
          } else {
            console.log('Service with ID not found:', serviceID);
          }
        } else {
          console.error('Service not found for ID:', serviceID);
        }
      },
      error => {
        console.error('API Error : ', error);
      }
    );
  }
  //getting the customer name
  fetchCustomerName(customerID:string){
    this.apiCustomerServices.getAllCustomers().subscribe(
      customer => {
        if (customer) {
          // console.log(customer);
          const foundService = customer.find(Customer => Customer.id === customerID);
          if (foundService) {
            this.customerNames.set(customerID, foundService.firstName);
          } else {
            console.log('Customer with ID not found:', customerID);
          }
        } else {
          console.error('Customer not found for ID:', customerID);
        }
      },
      error => {
        console.error('API Error : ', error);
      }
    );
  }
  //getting the car details 
  fetchCarName(carID:string){
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

  cancelRequest(serviceRequestId: string){
    console.log('id',serviceRequestId);
    if(serviceRequestId){
      this.apiRequestService.getAllRequests().subscribe(
          serviceReuest=>{
            if(serviceReuest){
              console.log('full object',serviceReuest);
              const foundServiceRequest = serviceReuest.find(Request=> Request.id === serviceRequestId);
              console.log('id match',foundServiceRequest);
              if(foundServiceRequest){
                const update={
                  id: foundServiceRequest.id,
                  status: '3 ', //'3' represents the status for 'Cancelled'
                  date: foundServiceRequest.date,
                  //  location:foundServiceRequest.location,
                  time:foundServiceRequest.time,
                  comment: foundServiceRequest.comment,
                  serviceId: foundServiceRequest.serviceId,
                  customerId: foundServiceRequest.customerId,
                  employeeId: foundServiceRequest.employeeId,
                  carId: foundServiceRequest.carId,
                  adminId: foundServiceRequest.adminId
                }
                this.apiRequestService.updateRequest(serviceRequestId,update).subscribe(
                  ()=>{
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
  assignEmployee(serviceRequestId: string){
    console.log(serviceRequestId);
    this.router.navigate(['/EditRequest',serviceRequestId]);
  }

}