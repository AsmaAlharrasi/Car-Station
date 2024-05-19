import { TestBed } from '@angular/core/testing';

import { ApiEployeeService } from './api-eployee.service';

describe('ApiEployeeService', () => {
  let service: ApiEployeeService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ApiEployeeService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
