import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { Services } from '../../datastore/services';
import { ApiCarServicesService } from '../../services/carServices/api-car-services.service';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule, MatLabel } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';

@Component({
  selector: 'app-add-service',
  standalone: true,
  imports: [ 
     RouterLink,
    ReactiveFormsModule,
    // CommonModule,
    MatFormFieldModule,
    MatButtonModule,
    MatInputModule,
    MatLabel],
  templateUrl: './add-service.component.html',
  styleUrl: './add-service.component.scss'
})
export class AddServiceComponent {
  AddServiceForm!: FormGroup;
  service: Services | any;

  constructor(private apiCarserviceservice: ApiCarServicesService, private builder: FormBuilder, private route: Router) {
  
    this.AddServiceForm = this.builder.group({
      type: ['', Validators.required],
      price: ['', Validators.required],
      description: ['', Validators.required],
      adminId: ['3FA85F64-5717-4562-B3FC-2C963F66AFA6']

    });
  }


  onSubmit(): void {
    if (this.AddServiceForm.valid) {
      const item = this.AddServiceForm.value;

      this.apiCarserviceservice.createService(item)
        .subscribe(
          (createItem) => {
            console.log('Service created:', createItem);
            this.route.navigate(['AdminViewServices']);
          },
          (error) => {
            console.error('Error creating Car:', error);
          }
        );

    } else {
      console.error('Form is invalid');
    }
  }

}
