import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LotHistorycomponent } from './lot-historycomponent';

describe('LotHistorycomponent', () => {
  let component: LotHistorycomponent;
  let fixture: ComponentFixture<LotHistorycomponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LotHistorycomponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LotHistorycomponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
