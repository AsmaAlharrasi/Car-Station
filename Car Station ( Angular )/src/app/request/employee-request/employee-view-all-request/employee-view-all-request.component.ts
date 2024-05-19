import { Request } from './../../../datastore/request';
import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { ApiRequesService } from '../../../services/request/api-reques.service';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { ApiCarServicesService } from '../../../services/carServices/api-car-services.service';
import { ApiCarsService } from '../../../services/cars/api-cars.service';

@Component({
  selector: 'app-employee-view-all-request',
  standalone: true,
  imports: [RouterLink, CommonModule, FormsModule, ReactiveFormsModule],
  templateUrl: './employee-view-all-request.component.html',
  styleUrl: './employee-view-all-request.component.scss'
})
export class EmployeeViewAllRequestComponent implements OnInit {

  employeeRequests: Request | any = [];
  statusGroup: FormGroup;
  request : Request | any ;

  serviceNames: Map<string, string> = new Map();
  carNames: Map<string, string> = new Map();



  constructor(private ApiRequesService: ApiRequesService,
    private route: ActivatedRoute,
    private router: Router,
    private fb: FormBuilder,
    private apiCarServices : ApiCarServicesService,
    private apiCarService : ApiCarsService,) {
    this.statusGroup = this.fb.group({
      requestStatus: ['', Validators.required],
    });

  }

  ngOnInit(): void {
    const EmpID = String(this.route.snapshot.paramMap.get('EmpId'));

    this.ApiRequesService.getAllRequests().subscribe(
      (allEmpRequest: Request[]) => {
        this.employeeRequests = allEmpRequest.filter(empReq => empReq.employeeId == EmpID);
        
        allEmpRequest.forEach(empReq =>{
          this.fetchServiceName(empReq.serviceId);
          this.fetchCarName(empReq.carId);
  
        });
        console.log(allEmpRequest);
      }
    );

  }
 

  // Method to update the status of a request
  updateStatus(index: number, requestId : string) {
    const EmpID = String(this.route.snapshot.paramMap.get('EmpId'));

    const val = this.statusGroup.get('requestStatus')?.value;


    if(requestId){
      this.ApiRequesService.getAllRequests().subscribe(
          serviceReuest=>{
            if(serviceReuest){
              console.log('full object',serviceReuest);
              const foundRequest = serviceReuest.find(Request=> Request.id === requestId);
              console.log('id match',foundRequest);
              if(foundRequest){

                const update={
                  id: foundRequest.id,
                  status: val, 
                  date: foundRequest.date,
                  time:foundRequest.time,
                  comment: foundRequest.comment,
                  serviceId: foundRequest.serviceId,
                  customerId: foundRequest.customerId,
                  employeeId: foundRequest.employeeId,
                  carId: foundRequest.carId,
                  adminId: foundRequest.adminId
                }

                this.ApiRequesService.updateRequest(requestId,update).subscribe(
                  ()=>{
                    console.log('status updated , ', foundRequest);
                    console.log('new status : ', foundRequest.status);
                  }
                );
              }
            }
          }
      );
    }

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
  
}
 




