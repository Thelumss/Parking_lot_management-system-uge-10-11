import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Lotcomponent } from './lotcomponent';

describe('Lotcomponent', () => {
  let component: Lotcomponent;
  let fixture: ComponentFixture<Lotcomponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Lotcomponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Lotcomponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
