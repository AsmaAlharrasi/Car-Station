import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewAllServiceComponent } from './view-all-service.component';

describe('ViewAllServiceComponent', () => {
  let component: ViewAllServiceComponent;
  let fixture: ComponentFixture<ViewAllServiceComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ViewAllServiceComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ViewAllServiceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
