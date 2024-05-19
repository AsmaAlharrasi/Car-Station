import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CustomerAddRequestComponent } from './customer-add-request.component';

describe('CustomerAddRequestComponent', () => {
  let component: CustomerAddRequestComponent;
  let fixture: ComponentFixture<CustomerAddRequestComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CustomerAddRequestComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CustomerAddRequestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
