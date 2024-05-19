import { Component } from '@angular/core';
import { NewRequestComponent } from './new-request/new-request.component';
import { AssignedRequestsComponent } from './assigned-requests/assigned-requests.component';
import { CanceledRequestsComponent } from './canceled-requests/canceled-requests.component';

@Component({
  selector: 'app-admin-view-all-request',
  standalone: true,
  imports: [
    NewRequestComponent,
    AssignedRequestsComponent,
    CanceledRequestsComponent
  ],
  templateUrl: './admin-view-all-request.component.html',
  styleUrl: './admin-view-all-request.component.scss'
})
export class AdminViewAllRequestComponent {

}
