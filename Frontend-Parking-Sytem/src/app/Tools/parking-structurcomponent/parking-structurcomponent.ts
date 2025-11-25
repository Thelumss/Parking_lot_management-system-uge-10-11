import { LiveAnnouncer } from '@angular/cdk/a11y';
import { Component, inject, ViewChild } from '@angular/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInput } from '@angular/material/input';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatSort, MatSortModule, Sort } from '@angular/material/sort';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { ParkingStrucursService } from '../../Services/parking-strucurs-service';
import { MatDialog } from '@angular/material/dialog';
import { Route, Router } from '@angular/router';
import { EditDialog } from '../../edit-dialog/edit-dialog';
import { DynamicTableComponet } from "../../Shared/dynamic-table-componet/dynamic-table-componet";



export interface Parking_strutur {
  parking_lot_Structur_ID: number;
  name: string;
  adress: string;
  total_Available_Lots: number;
  total_Occupied_Lots: number;
  basePrice: number;
}


@Component({
  selector: 'app-parking-structurcomponent',
  standalone: true,
  imports: [MatTableModule, MatPaginatorModule, MatSortModule, MatFormFieldModule, DynamicTableComponet],
  templateUrl: './parking-structurcomponent.html',
  styleUrl: './parking-structurcomponent.css',
})
export class ParkingStructurcomponent {



  // the constructor createing ProductApiCallServices for use  
  constructor(private api: ParkingStrucursService, private dialog: MatDialog, private router: Router) { }

  displayedColumns = [
    'name',
    'adress',
    'total_Available_Lots',
    'total_Occupied_Lots',
    'basePrice'
  ];


  products: any[] = [];

  @ViewChild(DynamicTableComponet) dynamicTable!: DynamicTableComponet;

  ngAfterViewInit() {
    this.loadParkingLotStrucurs();
  }

  // this makes a api call that get all of the Product information that we would want
  loadParkingLotStrucurs() {
    this.api.getparking_Lot_Structur().subscribe({
      next: res => {
        this.products = res;
      },
      error: err => console.error('API error:', err)
    });
  }

  onEdit(row: any) {
    const dialogRef = this.dialog.open(EditDialog, {
      width: '400px',
      data: row,
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        console.log(result);
        this.api.UpdateParking_Lot_Structur(result).subscribe(() => {
          this.loadParkingLotStrucurs();  // Reload the updated data
        });
      }
    });
  }

  Delete(element: any) {
    if (!confirm(`Are you sure you want to delete "${element.name}"?`)) {
      return; // user canceled
    }

    this.api.DeleteParking_Lot_Structur(element.parking_lot_Structur_ID)
      .subscribe(() => {

      });
  }

  onRowDoubleClick(row: any) {
    this.openDetails(row);  // Use the openDetails method to navigate
  }

  openDetails(row: any) {
    this.router.navigate(['/lots', row.parking_lot_Structur_ID]);
  }

}
