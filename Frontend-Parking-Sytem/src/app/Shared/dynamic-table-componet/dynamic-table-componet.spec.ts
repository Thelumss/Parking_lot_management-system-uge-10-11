import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DynamicTableComponet } from './dynamic-table-componet';

describe('DynamicTableComponet', () => {
  let component: DynamicTableComponet;
  let fixture: ComponentFixture<DynamicTableComponet>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DynamicTableComponet]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DynamicTableComponet);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
