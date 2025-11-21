import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserLogincomponent } from './user-logincomponent';

describe('UserLogincomponent', () => {
  let component: UserLogincomponent;
  let fixture: ComponentFixture<UserLogincomponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UserLogincomponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UserLogincomponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
