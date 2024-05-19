import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminViewAllServiceComponent } from './admin-view-all-service.component';

describe('AdminViewAllServiceComponent', () => {
  let component: AdminViewAllServiceComponent;
  let fixture: ComponentFixture<AdminViewAllServiceComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AdminViewAllServiceComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AdminViewAllServiceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
