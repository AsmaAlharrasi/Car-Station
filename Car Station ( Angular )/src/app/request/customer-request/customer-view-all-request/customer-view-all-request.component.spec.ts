import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CustomerViewAllRequestComponent } from './customer-view-all-request.component';

describe('CustomerViewAllRequestComponent', () => {
  let component: CustomerViewAllRequestComponent;
  let fixture: ComponentFixture<CustomerViewAllRequestComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CustomerViewAllRequestComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CustomerViewAllRequestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
