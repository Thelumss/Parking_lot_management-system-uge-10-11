import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CarIncomponent } from './car-incomponent';

describe('CarIncomponent', () => {
  let component: CarIncomponent;
  let fixture: ComponentFixture<CarIncomponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CarIncomponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CarIncomponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
