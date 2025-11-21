import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Toolbarcomponent } from './toolbarcomponent';

describe('Toolbarcomponent', () => {
  let component: Toolbarcomponent;
  let fixture: ComponentFixture<Toolbarcomponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Toolbarcomponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Toolbarcomponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
