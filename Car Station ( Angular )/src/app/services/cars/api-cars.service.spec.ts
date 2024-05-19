import { TestBed } from '@angular/core/testing';

import { ApiCarsService } from './api-cars.service';

describe('ApiCarsService', () => {
  let service: ApiCarsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ApiCarsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
