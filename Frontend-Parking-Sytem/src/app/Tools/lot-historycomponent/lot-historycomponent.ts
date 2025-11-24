import { LiveAnnouncer } from '@angular/cdk/a11y';
import { Component, inject, ViewChild } from '@angular/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInput } from '@angular/material/input';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatSort, MatSortModule, Sort } from '@angular/material/sort';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { ParkingStrucursService } from '../../Services/parking-strucurs-service';
import { LotHistoryService } from '../../Services/lot-history-service';

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
  imports: [MatTableModule, MatPaginatorModule,MatSortModule,MatInput,MatFormFieldModule],
  templateUrl: './lot-historycomponent.html',
  styleUrl: './lot-historycomponent.css',
})
export class LotHistorycomponent {
applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }
  
  // the constructor createing ProductApiCallServices for use  
  constructor(private api: LotHistoryService) { }

  private _liveAnnouncer = inject(LiveAnnouncer);
  // displayedColumns sets up how many collumns there is needed and what they should contatin
  displayedColumns: string[] = ['license_PLate_Numbers', 'entry_time', 'exit_time','charged','active'];
  // dataSource is what holds the data that displayedColumns shows
  dataSource = new MatTableDataSource<Product>([]);
  // products holds the admins for when they should go over to dataSource
  products: any[] = [];

  @ViewChild(MatSort) sort!: MatSort;
  @ViewChild(MatPaginator) paginator!: MatPaginator;

  ngAfterViewInit() {
    this.dataSource.sort = this.sort;
    this.dataSource.paginator = this.paginator;
    this.loadLotHistory();
  }

announceSortChange(sortState: Sort) {
    if (sortState.direction) {
      this._liveAnnouncer.announce(`Sorted ${sortState.direction}ending`);
    } else {
      this._liveAnnouncer.announce('Sorting cleared');
    }
  }

  // this makes a api call that get all of the Product information that we would want
  loadLotHistory() {
    this.api.getparking_Lot_Structur().subscribe({
      next: res => {
        this.products = res;
        this.dataSource.data = this.products;
        this.dataSource.sort = this.sort;
        this.dataSource.paginator = this.paginator;
      },
      error: err => console.error('API error:', err)
    });
  }

}
