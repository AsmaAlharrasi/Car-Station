import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EmployeeViewAllRequestComponent } from './employee-view-all-request.component';

describe('EmployeeViewAllRequestComponent', () => {
  let component: EmployeeViewAllRequestComponent;
  let fixture: ComponentFixture<EmployeeViewAllRequestComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EmployeeViewAllRequestComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(EmployeeViewAllRequestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
