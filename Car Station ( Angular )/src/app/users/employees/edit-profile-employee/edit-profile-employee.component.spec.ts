import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditProfileEmployeeComponent } from './edit-profile-employee.component';

describe('EditProfileEmployeeComponent', () => {
  let component: EditProfileEmployeeComponent;
  let fixture: ComponentFixture<EditProfileEmployeeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EditProfileEmployeeComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(EditProfileEmployeeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
