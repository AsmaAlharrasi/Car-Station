import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';import { Router, RouterLink } from '@angular/router';
import { ApiRequesService } from '../../../../services/request/api-reques.service';
import { MatTableModule } from '@angular/material/table';
import { Request } from '../../../../datastore/request';
import { ApiCarServicesService } from '../../../../services/carServices/api-car-services.service';
import { ApiCustomerService } from '../../../../services/customer/api-customer.service';
import { ApiCarsService } from '../../../../services/cars/api-cars.service';
import { ApiEployeeService } from '../../../../services/employee/api-eployee.service';


@Component({
  selector: 'app-assigned-requests',
  standalone: true,
  imports: [RouterLink,CommonModule,MatTableModule],
  templateUrl: './assigned-requests.component.html',
  styleUrl: './assigned-requests.component.scss'
})
export class AssignedRequestsComponent implements OnInit {
  displayedColumns: string[] = ['customerName','serviceType','date','carVIN', 'comment','status','employeeId','Action'];
  sRequest :Request[]=[];

  serviceNames: Map<string, string> = new Map();
  customerNames: Map<string, string> = new Map();
  carNames: Map<string, string> = new Map();
  employeeName: Map<string, string> = new Map();




  constructor(private apiRequestService: ApiRequesService,
              private apiCarServices : ApiCarServicesService,
              private apiCustomerServices : ApiCustomerService,
              private apiCarService: ApiCarsService,
              private ApiEployeeService: ApiEployeeService,
              private router: Router){}

  ngOnInit(): void {

    this.loadTheData();
  
  }
  loadTheData(){
    this.apiRequestService.getAllRequests().subscribe(sRequest=>{
      this.sRequest = sRequest.filter(sRequest => sRequest.employeeId != null && sRequest.status != 'Canceled' );
      console.log('Filtered requests:', this.sRequest);
      // this.sRequest=sRequest;
      sRequest.forEach(sRequest =>{
        this.fetchServiceName(sRequest.serviceId);
        this.fetchCustomerName(sRequest.customerId);
        this.fetchCarName(sRequest.carId);
        this.fetchEmployeeName(sRequest.employeeId);

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
          const foundCustomer = customer.find(Customer => Customer.id === customerID);
          if (foundCustomer) {
            this.customerNames.set(customerID, foundCustomer.firstName);
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
          const foundCar = Car.find(Car => Car.id === carID);
          if (foundCar) {
            this.carNames.set(carID, foundCar.vin);
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
  //get employee name 
  fetchEmployeeName(employeeID:string){
    this.ApiEployeeService.getEmployees().subscribe(
      Employee => {
        if (Employee) {
          // console.log(Car);
          const foundEmployee = Employee.find(Employee => Employee.id === employeeID);
          if (foundEmployee) {
            this.employeeName.set(employeeID, foundEmployee.firstName);
            // console.log('Car ID:', carID);
          } else {
            console.log('Car with ID not found:', employeeID);
          }
        } else {
          console.error('Car not found for ID:', employeeID);
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
              //console.log('id match',foundServiceRequest);
              if(foundServiceRequest){
                const update={
                  id: foundServiceRequest.id,
                  status:' 3' , //'3' represents the status for 'Cancelled'
                  date: foundServiceRequest.date,
                  //  location:foundServiceRequest.location,
                  comment: foundServiceRequest.comment,
                  time: foundServiceRequest.time,
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
}