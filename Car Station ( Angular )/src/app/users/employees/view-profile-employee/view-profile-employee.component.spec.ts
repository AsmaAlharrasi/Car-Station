import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewProfileEmployeeComponent } from './view-profile-employee.component';

describe('ViewProfileEmployeeComponent', () => {
  let component: ViewProfileEmployeeComponent;
  let fixture: ComponentFixture<ViewProfileEmployeeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ViewProfileEmployeeComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ViewProfileEmployeeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
