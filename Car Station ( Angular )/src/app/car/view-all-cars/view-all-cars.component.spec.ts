import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewAllCarsComponent } from './view-all-cars.component';

describe('ViewAllCarsComponent', () => {
  let component: ViewAllCarsComponent;
  let fixture: ComponentFixture<ViewAllCarsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ViewAllCarsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ViewAllCarsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
