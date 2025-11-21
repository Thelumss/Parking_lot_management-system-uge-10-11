import { TestBed } from '@angular/core/testing';

import { LotTypesService } from './lot-types-service';

describe('LotTypesService', () => {
  let service: LotTypesService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(LotTypesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
