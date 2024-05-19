import { Employee } from './../../../datastore/employee';
import { Component, OnInit } from '@angular/core';
import { RouterLink, ActivatedRoute, Router } from '@angular/router';
import { ApiEployeeService } from '../../../services/employee/api-eployee.service';
import { FormBuilder, FormGroup, FormsModule, Validators, ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule, MatLabel } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';


@Component({
  selector: 'app-edit-profile-employee',
  standalone: true,
  imports: [
    RouterLink,
    ReactiveFormsModule,
    MatFormFieldModule,
    CommonModule,
    MatButtonModule,
    MatInputModule,
    MatLabel,
    MatSelectModule],
    
  templateUrl: './edit-profile-employee.component.html',
  styleUrl: './edit-profile-employee.component.scss'
})
export class EditProfileEmployeeComponent implements OnInit {

  employee: Employee | any;
  employeeGroup: FormGroup;


  constructor(private ApiEmployeeService: ApiEployeeService,
    private ActivatedRoute: ActivatedRoute,
    private route: Router,
    private fb: FormBuilder)
    
    {
    this.employeeGroup = this.fb.group({
      firstName: ['', Validators.required],
      middleName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', Validators.required],
      gender: ['', Validators.required],
      phoneNumber: ['', Validators.required],
      salary: ['', Validators.required],
      birthDate: [''],
      adminId: ['3fa85f64-5717-4562-b3fc-2c963f66afa6', Validators.required],
      employeeStatus: ['', Validators.required],
      userID: ['2']
    }
    );

  }

  ngOnInit(): void {
    const EmpID = String(this.ActivatedRoute.snapshot.paramMap.get('EmpId'));

    this.ApiEmployeeService.getEmployee(EmpID).subscribe(
      (employee: Employee) => {
        this.employee = employee;
        console.log(this.employee); // Do something with the retrieved book
        const birthDateFormatted = this.employee.birthDate ? new Date(this.employee.birthDate).toISOString().split('T')[0] : '';

        this.employeeGroup.patchValue({
          firstName: this.employee.firstName,
          middleName: this.employee.middleName,
          lastName: this.employee.lastName,
          email: this.employee.email,
          gender: this.employee.gender,
          phoneNumber: this.employee.phoneNumber,
          salary: this.employee.salary,
          birthDate: birthDateFormatted,
          employeeStatus: this.employee.employeeStatus,
        });
      }
    );

  }

  onSubmit(): void {
    if (this.employeeGroup.valid) {
      const newEmployee = this.employeeGroup.value;
      newEmployee.Id = this.employee.id;
      console.log(newEmployee.id);
      this.ApiEmployeeService.updateEmployee(newEmployee.Id, newEmployee).subscribe(
        (x: Employee) => {
          console.log('Employee Updated Successfully:', x);

          // Reset the form after successful submission
          this.employeeGroup.reset();
          this.route.navigate(['ViewAllEmployeeComponent']);
        },
        (error) => {
          console.error('Error Updating Employee:', error);
        }
      );
    } else {
      console.log("Form is inValid");
    }
  }


}
