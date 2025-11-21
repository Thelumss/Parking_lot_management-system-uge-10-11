import { TestBed } from '@angular/core/testing';

import { LotHistoryService } from './lot-history-service';

describe('LotHistoryService', () => {
  let service: LotHistoryService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(LotHistoryService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
