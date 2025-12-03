import { Component } from '@angular/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInput } from '@angular/material/input';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatTableModule } from '@angular/material/table';
import { LotHistoryService } from '../../../Services/lot-history-service';
import { DynamicTableComponet } from "../../Shared/dynamic-table-componet/dynamic-table-componet";

export interface LotHistory {
  license_PLate_Numbers: string;
  entry_time: number;
  exit_time: number;
  charged: number;
  active: string;
  parking_Lot_Structur_Name: string;
  lot_Type: string

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


  displayedColumns = [
    { key: 'license_PLate_Numbers', label: 'License plate numbers' },
    { key: 'entry_time', label: 'Entry time' },
    { key: 'exit_time', label: 'Exit time' },
    { key: 'charged', label: 'Charged' },
    { key: 'parking_Lot_Structur_Name', label: 'Parking lot structur name' },
    { key: 'lot_Type', label: 'Lot type' },
    { key: 'active', label: 'Active' },
  ];

  products: any[] = [];


  ngAfterViewInit() {
    this.loadLotHistory();
  }

  // this makes a api call that get all of the Product information that we would want
  loadLotHistory() {
    this.api.getparking_Lot_Structur().subscribe({
      next: res => {
        // Convert entry_time and exit_time from milliseconds to human-readable date
        this.products = res.map((lotHistory: LotHistory) => {
          return {
            ...lotHistory,
            entry_time: new Date(lotHistory.entry_time).toLocaleString(),
            exit_time: lotHistory.exit_time
              ? new Date(lotHistory.exit_time).toLocaleString()
              : 'N/A',
            active: lotHistory.active ? 'Yes' : 'No',  // <-- Convert boolean to string
          };
        });
      },
      error: err => console.error('API error:', err)
    });
  }
}