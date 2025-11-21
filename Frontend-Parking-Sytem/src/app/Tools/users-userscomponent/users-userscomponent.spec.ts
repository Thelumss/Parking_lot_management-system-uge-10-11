import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UsersUserscomponent } from './users-userscomponent';

describe('UsersUserscomponent', () => {
  let component: UsersUserscomponent;
  let fixture: ComponentFixture<UsersUserscomponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UsersUserscomponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UsersUserscomponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
