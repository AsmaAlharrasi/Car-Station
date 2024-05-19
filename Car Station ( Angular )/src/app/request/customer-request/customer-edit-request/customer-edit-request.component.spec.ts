import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CustomerEditRequestComponent } from './customer-edit-request.component';

describe('CustomerEditRequestComponent', () => {
  let component: CustomerEditRequestComponent;
  let fixture: ComponentFixture<CustomerEditRequestComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CustomerEditRequestComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CustomerEditRequestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
