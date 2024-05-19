import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { MatTableModule } from '@angular/material/table';
import { RouterLink, Router, ActivatedRoute } from '@angular/router';
import { ApiEployeeService } from '../../../services/employee/api-eployee.service';
import { Employee } from '../../../datastore/employee';
import { ApiRequesService } from '../../../services/request/api-reques.service';

@Component({
  selector: 'app-admin-edit-request',
  standalone: true,
  imports: [RouterLink, CommonModule, MatTableModule],
  templateUrl: './admin-edit-request.component.html',
  styleUrl: './admin-edit-request.component.scss'
})
export class AdminEditRequestComponent implements OnInit {

  displayedColumns: string[] = ['Full Name', 'Gender', 'Birth Date', 'Phone Number', 'Email', 'Action'];
  Employees: Employee[] = [];
  requestID = String(this.ActivatedRoute.snapshot.paramMap.get('Id'));


  constructor(
    private ApiEployeeService: ApiEployeeService,
    private apiRequestService: ApiRequesService,
    private ActivatedRoute: ActivatedRoute,
    private route: Router) { }

  ngOnInit(): void {
    // const requestID = String(this.ActivatedRoute.snapshot.paramMap.get('Id'));
    // console.log('request id: ',this.requestID);

    this.ApiEployeeService.getEmployees().subscribe(employee => {
      this.Employees = employee.filter(employee => employee.employeeStatus == 'Free');
      console.log('Filtered requests:', this.Employees);
    },
      (error) => {
        console.error('API Error: ', error);
      });

    //  requestID = String(this.ActivatedRoute.snapshot.paramMap.get('Id'));
    // console.log('request id: ',requestID);
  }

  updateRequest(employeeID: string) {
    console.log(employeeID, "request id : ", this.requestID);
    if (employeeID) {
      this.apiRequestService.getAllRequests().subscribe(
        serviceReuest => {
          if (serviceReuest) {
            console.log('full object', serviceReuest);
            const foundServiceRequest = serviceReuest.find(Request => Request.id === this.requestID);
            //console.log('id match',foundServiceRequest);
            if (foundServiceRequest) {
              const update = {
                id: foundServiceRequest.id,
                status: foundServiceRequest.status, //'3' represents the status for 'Cancelled'
                date: foundServiceRequest.date,
                comment: foundServiceRequest.comment,
                time: foundServiceRequest.time,
                serviceId: foundServiceRequest.serviceId,
                customerId: foundServiceRequest.customerId,
                employeeId: employeeID,
                carId: foundServiceRequest.carId,
                adminId: foundServiceRequest.adminId
              }
              this.apiRequestService.updateRequest(employeeID, update).subscribe(
                () => {
                  console.log('Employee is assigned  , ', foundServiceRequest);
                  console.log('new status : ', foundServiceRequest.status);
                  this.route.navigate(['ViewRequest']);
                  // this.loadTheData();
                }
              )
            }
          }
        }
      );
    }
  }

}
