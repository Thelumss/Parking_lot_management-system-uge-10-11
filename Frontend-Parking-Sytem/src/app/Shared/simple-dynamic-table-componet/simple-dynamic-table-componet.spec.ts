import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SimpleDynamicTableComponet } from './simple-dynamic-table-componet';

describe('SimpleDynamicTableComponet', () => {
  let component: SimpleDynamicTableComponet;
  let fixture: ComponentFixture<SimpleDynamicTableComponet>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SimpleDynamicTableComponet]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SimpleDynamicTableComponet);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
