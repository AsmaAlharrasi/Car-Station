import { Employee } from './../../../datastore/employee';
import { Router, RouterLink } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ApiEployeeService } from '../../../services/employee/api-eployee.service';
import { MatTableModule } from '@angular/material/table';


@Component({
  selector: 'app-view-all-employee',
  standalone: true,
  imports: [RouterLink, CommonModule, MatTableModule],
  templateUrl: './view-all-employee.component.html',
  styleUrl: './view-all-employee.component.scss'
})
export class ViewAllEmployeeComponent implements OnInit {

  constructor(private ApiEployeeService: ApiEployeeService, private route: Router ) { }

  /* The Array is of Type Any */
  
  displayedColumns: string[] = [ 'Full Name','Email','Gender','Birth Date','Phone Number','Salary','Action'];
  Employees:Employee[]=[];


  ngOnInit(): void {
    this.ApiEployeeService.getEmployees().subscribe(
      (Employees: Employee[]) => {
        this.Employees = Employees;
        /* CHECK THIS*/
        if(Employees.length > 0) {
          console.log(Employees); // Only access Name if there are Employees
          console.log("Employees found."); // Only access Name if there are Employees

        } else {
          console.log("No Employees found."); // Handle the case where no Employees are retrieved
        }
      }
    );
  }

  deleteEmployee(employee: Employee): void {
    console.log(employee);

    const isConfirmed = confirm(`Are you sure you want to delete ${employee.firstName} with id ${employee.id} ?`);
    if (isConfirmed) {
      this.ApiEployeeService.deleteEmployee(employee.id).subscribe(() => {
        this.Employees = this.Employees.filter(c => c.id !== employee.id);
      });
    }
  }

}
