import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CanceledRequestsComponent } from './canceled-requests.component';

describe('CanceledRequestsComponent', () => {
  let component: CanceledRequestsComponent;
  let fixture: ComponentFixture<CanceledRequestsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CanceledRequestsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CanceledRequestsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
