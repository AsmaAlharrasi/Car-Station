import { Employee } from './../../../datastore/employee';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { ApiEployeeService } from '../../../services/employee/api-eployee.service';
import { AbstractControl, FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule, MatLabel } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';

@Component({
  selector: 'app-add-employee',
  standalone: true,
  imports: [RouterLink, ReactiveFormsModule,
    MatFormFieldModule,
    CommonModule,
    MatButtonModule,
    MatLabel,
    MatInputModule,
    MatSelectModule,
  ],
  templateUrl: './add-employee.component.html',
  styleUrl: './add-employee.component.scss'
})

export class AddEmployeeComponent implements OnInit {

  employeeGroup: FormGroup;

  constructor(private ApiEployeeService: ApiEployeeService, private route: Router, private fb: FormBuilder) {
    this.employeeGroup = this.fb.group({
      firstName: ['', Validators.required],
      middleName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', Validators.required],
      gender: ['', Validators.required],
      phoneNumber: ['', Validators.required], 
      salary: ['',Validators.required], 
      DoB : ['', Validators.required],
      AdminId : ['3fa85f64-5717-4562-b3fc-2c963f66afa6', Validators.required],
      EmployeeStatus : [0, Validators.required],
      UserID : ['2']
    }
    );
  }


  ngOnInit(): void {
    
  }

  onSubmit(): void {
    if (this.employeeGroup.valid) {
    const newEmployee = this.employeeGroup.value;
    this.ApiEployeeService.addEmployee(newEmployee).subscribe(
      (x:Employee) => {
        console.log('Employee added successfully:', x);
        // Reset the form after successful submission
        this.employeeGroup.reset();
        this.route.navigate(['/ViewAllEmployeeComponent']);
      },
      (error) => {
        console.error('Error adding Employee:', error);
      }
    );
  } else{
    console.log("Form is inValid");
  }
}

}