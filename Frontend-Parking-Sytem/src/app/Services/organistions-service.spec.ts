import { TestBed } from '@angular/core/testing';

import { OrganistionsService } from './organistions-service';

describe('OrganistionsService', () => {
  let service: OrganistionsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(OrganistionsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
