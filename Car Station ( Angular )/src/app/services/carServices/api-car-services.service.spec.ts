import { TestBed } from '@angular/core/testing';

import { ApiCarServicesService } from './api-car-services.service';

describe('ApiCarServicesService', () => {
  let service: ApiCarServicesService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ApiCarServicesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
