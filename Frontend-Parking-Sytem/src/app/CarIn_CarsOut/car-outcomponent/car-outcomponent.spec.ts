import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CarOutcomponent } from './car-outcomponent';

describe('CarOutcomponent', () => {
  let component: CarOutcomponent;
  let fixture: ComponentFixture<CarOutcomponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CarOutcomponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CarOutcomponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
