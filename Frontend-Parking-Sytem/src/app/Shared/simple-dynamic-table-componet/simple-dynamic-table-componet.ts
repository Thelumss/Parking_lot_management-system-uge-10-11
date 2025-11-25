import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-simple-dynamic-table-componet',
  imports: [],
  templateUrl: './simple-dynamic-table-componet.html',
  styleUrl: './simple-dynamic-table-componet.css',
})
export class SimpleDynamicTableComponet {

  @Input() columns: string[] = [];
  @Input() data: any[] = [];

}
