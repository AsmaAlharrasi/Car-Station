import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { Services } from '../../datastore/services';
import { ApiCarServicesService } from '../../services/carServices/api-car-services.service';
import { MatFormFieldModule, MatLabel } from '@angular/material/form-field';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';

@Component({
  selector: 'app-edit-service',
  standalone: true,
  imports: [ 
    RouterLink,
    ReactiveFormsModule,
    MatFormFieldModule,
    CommonModule,
    MatButtonModule,
    MatInputModule,
    MatLabel],
  templateUrl: './edit-service.component.html',
  styleUrl: './edit-service.component.scss'
})
export class EditServiceComponent implements OnInit {

service: Services | any;
UpdateServiceForm!: FormGroup;
Id!: string | null;

constructor(
  private apiCarServiceServices: ApiCarServicesService,
  private router: Router, private builder: FormBuilder,
  private route: ActivatedRoute
) { }

ngOnInit(): void {
  this.Id = this.route.snapshot.paramMap.get('Id') as string | null;

  this.UpdateServiceForm = this.builder.group({
    type: [''],
    price: [''],
    description: [''],
    adminId: ['3fa85f64-5717-4562-b3fc-2c963f66afa6']
  


  });

  if (this.Id) {
    this.apiCarServiceServices.getService(this.Id).subscribe(service => {
      this.service = service;
      //Bug
      console.log(this.service);
      this.UpdateServiceForm.patchValue(service);
    });
  }
}

onSubmit(): void {

  if (this.UpdateServiceForm.valid) {
    const service = this.UpdateServiceForm.value;
    service.Id = this.Id;


    this.apiCarServiceServices.updateService(service.Id, service).subscribe(
      () => {
        console.log(`Car with ID ${service.Id} updated successfully`)
        this.router.navigate(['AdminViewServices']);
      }, (error) => {
        console.error(`Error updating Car with ID ${service.Id}:`, error);

      }
    );
  }
}

}