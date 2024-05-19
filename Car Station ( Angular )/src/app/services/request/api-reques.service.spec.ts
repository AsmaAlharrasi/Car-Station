import { TestBed } from '@angular/core/testing';

import { ApiRequesService } from './api-reques.service';

describe('ApiRequesService', () => {
  let service: ApiRequesService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ApiRequesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
