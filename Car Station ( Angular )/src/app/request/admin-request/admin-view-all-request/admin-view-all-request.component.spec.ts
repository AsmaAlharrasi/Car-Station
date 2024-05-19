import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminViewAllRequestComponent } from './admin-view-all-request.component';

describe('AdminViewAllRequestComponent', () => {
  let component: AdminViewAllRequestComponent;
  let fixture: ComponentFixture<AdminViewAllRequestComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AdminViewAllRequestComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AdminViewAllRequestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
