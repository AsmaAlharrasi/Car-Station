import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditProfileCustomerComponent } from './edit-profile-customer.component';

describe('EditProfileCustomerComponent', () => {
  let component: EditProfileCustomerComponent;
  let fixture: ComponentFixture<EditProfileCustomerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EditProfileCustomerComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(EditProfileCustomerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
