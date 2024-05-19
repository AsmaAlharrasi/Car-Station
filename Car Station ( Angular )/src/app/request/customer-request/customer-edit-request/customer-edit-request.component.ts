import { Component, OnInit } from '@angular/core';
import { Request } from '../../../datastore/request';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ApiRequesService } from '../../../services/request/api-reques.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-customer-edit-request',
  standalone: true,
  imports: [],
  templateUrl: './customer-edit-request.component.html',
  styleUrl: './customer-edit-request.component.scss'
})
export class CustomerEditRequestComponent implements OnInit {
  request: Request | any;
  UpdateCarForm!: FormGroup;
  id!: number;

  constructor(
    private apiRequestServices: ApiRequesService,
    private router: Router, private formBuilder: FormBuilder,
    private route: ActivatedRoute
  ) { }

  ngOnInit(): void {
    const requestId = String(this.route.snapshot.params['id']);
    // console.log(id);

    this.UpdateCarForm = this.formBuilder.group({
      id: [''],
      status: ['', Validators.required],
      date: ['', Validators.required],
      time: ['', Validators.required],
      comment: ['', Validators.required],
      serviceType: ['', Validators.required],
     
    });

    if (requestId) {
      console.log(requestId);

      this.apiRequestServices.getRequest(requestId).subscribe(
        request => {
          this.request = request;

          if (this.request) {
            this.UpdateCarForm.patchValue(
              {
                id: this.request.id,
                status: this.request.status,
                date: this.request.date,
                time: this.request.time,
                comment: this.request.comment,
                serviceType: this.request.serviceType,
               
              });
          } else {
            console.error('Error : Request not found');
          }
        },
        (error) => {
          console.error('Error fetching Request:', error);
        }
      );
    }
  }
  UpdateData() {
    console.log("update");
  }

  
}
