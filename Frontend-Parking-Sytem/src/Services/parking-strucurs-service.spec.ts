import { TestBed } from '@angular/core/testing';

import { ParkingStrucursService } from './parking-strucurs-service';

describe('ParkingStrucursService', () => {
  let service: ParkingStrucursService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ParkingStrucursService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
