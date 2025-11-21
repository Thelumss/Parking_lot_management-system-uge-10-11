import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ParkingStructurcomponent } from './parking-structurcomponent';

describe('ParkingStructurcomponent', () => {
  let component: ParkingStructurcomponent;
  let fixture: ComponentFixture<ParkingStructurcomponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ParkingStructurcomponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ParkingStructurcomponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
