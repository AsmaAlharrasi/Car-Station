import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminEditRequestComponent } from './admin-edit-request.component';

describe('AdminEditRequestComponent', () => {
  let component: AdminEditRequestComponent;
  let fixture: ComponentFixture<AdminEditRequestComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AdminEditRequestComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AdminEditRequestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
