import { Component } from '@angular/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInput } from '@angular/material/input';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatTableModule } from '@angular/material/table';
import { LotHistoryService } from '../../../Services/lot-history-service';
import { DynamicTableComponet } from "../../Shared/dynamic-table-componet/dynamic-table-componet";

export interface Product {
  license_PLate_Numbers: string;
  entry_time: number;
  exit_time: number;
  charged: number;
  active: boolean;

}

@Component({
  selector: 'app-lot-historycomponent',
  standalone: true,
  imports: [MatTableModule, MatPaginatorModule, MatSortModule, MatInput, MatFormFieldModule, DynamicTableComponet],
  templateUrl: './lot-historycomponent.html',
  styleUrl: './lot-historycomponent.css',
})
export class LotHistorycomponent {

  // the constructor createing ProductApiCallServices for use  
  constructor(private api: LotHistoryService) { }


  displayedColumns: string[] = ['license_PLate_Numbers', 'entry_time', 'exit_time','charged','active'];

  products: any[] = [];


  ngAfterViewInit() {
    this.loadLotHistory();
  }

  // this makes a api call that get all of the Product information that we would want
loadLotHistory() {
    this.api.getparking_Lot_Structur().subscribe({
      next: res => {
        // Convert entry_time and exit_time from milliseconds to human-readable date
        this.products = res.map((product: Product) => {
          return {
            ...product,
            entry_time: new Date(product.entry_time).toLocaleString(),  // No need for *1000 if it's already in ms
            exit_time: product.exit_time ? new Date(product.exit_time).toLocaleString() : 'N/A',  // Handle null exit_time
          };
        });
      },
      error: err => console.error('API error:', err)
    });
  }
}