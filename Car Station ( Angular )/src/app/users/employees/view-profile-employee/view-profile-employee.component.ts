import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { Employee } from '../../../datastore/employee';
import { ApiEployeeService } from '../../../services/employee/api-eployee.service';

@Component({
  selector: 'app-view-profile-employee',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './view-profile-employee.component.html',
  styleUrl: './view-profile-employee.component.scss'
})
export class ViewProfileEmployeeComponent implements OnInit{
  employee : Employee | any;
 
   constructor(private ApiEployeeService : ApiEployeeService, private route : ActivatedRoute ){
 
   }

   ngOnInit(): void {
     const id = String(this.route.snapshot.paramMap.get('EmpId'));

     this.ApiEployeeService.getEmployee('3FA85F64-5717-4562-B3FC-2C963F66AFA1').subscribe(
       (employee : Employee) => 
         {
           this.employee = employee;
           console.log(employee);
         });
     
   }
  }