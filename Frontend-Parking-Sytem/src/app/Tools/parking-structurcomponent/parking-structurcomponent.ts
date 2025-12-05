import { Component, ViewChild } from '@angular/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatTableModule } from '@angular/material/table';
import { ParkingStrucursService } from '../../../Services/parking-strucurs-service';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
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
    { key: 'name', label: 'Name', readonly: false },
    { key: 'adress', label: 'Adress', readonly: false },
    { key: 'total_Available_Lots', label: 'Total available lots', readonly: true },
    { key: 'total_Occupied_Lots', label: 'Total occupied lots', readonly: true },
    { key: 'basePrice', label: 'BasePrice', readonly: false },
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
      width: '600px',   
      maxWidth: '90vw', 
      height: 'auto',   
      maxHeight: '90vh',
      data: {
        row: row,
        columns: this.displayedColumns,
        title: 'Edit Entry',
      },
      panelClass: 'custom-dialog-container'
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        const newParkingLot: Parking_strutur = {
          parking_lot_Structur_ID: result.row.parking_lot_Structur_ID,
          name: result.name ?? result.row.name,
          adress: result.adress ?? result.row.adress,
          basePrice: result.basePrice ?? result.row.basePrice,
          total_Available_Lots: result.row.total_Available_Lots,
          total_Occupied_Lots: result.row.total_Occupied_Lots
        };

        this.api.UpdateParking_Lot_Structur(newParkingLot).subscribe(() => {
          this.loadParkingLotStrucurs();
        });
      }
    });

  }

  onCreate() {
  const dialogRef = this.dialog.open(EditDialog, {
    width: '600px',
    maxWidth: '90vw',
    height: 'auto',
    maxHeight: '90vh',
    data: {
      row: {
        name: '',
        adress: '',
        basePrice: 0,
        total_Available_Lots: 0,
        total_Occupied_Lots: 0
      },
      columns: this.displayedColumns,
      title: 'Create',
      isNew: true
    },
    panelClass: 'custom-dialog-container'
  });

  dialogRef.afterClosed().subscribe(result => {
    if (result) {
      const newParkingLot: Parking_strutur = {
        parking_lot_Structur_ID: 0, // backend should assign ID
        name: result.name,
        adress: result.adress,
        basePrice: result.basePrice,
        total_Available_Lots: 0,
        total_Occupied_Lots: 0
      };

      this.api.CreateParking_Lot_Structur(newParkingLot).subscribe(() => {
      this.loadParkingLotStrucurs();
      });
    }
  });
}




  onDelete(element: any) {
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
