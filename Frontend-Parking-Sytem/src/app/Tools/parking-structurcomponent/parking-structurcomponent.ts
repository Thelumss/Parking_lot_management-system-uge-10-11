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
  imports: [MatTableModule, MatPaginatorModule,MatSortModule,MatInput,MatFormFieldModule],
  templateUrl: './parking-structurcomponent.html',
  styleUrl: './parking-structurcomponent.css',
})
export class ParkingStructurcomponent {


  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }
  
  // the constructor createing ProductApiCallServices for use  
  constructor(private api: ParkingStrucursService,private dialog: MatDialog,private router: Router) { }

  private _liveAnnouncer = inject(LiveAnnouncer);
  // displayedColumns sets up how many collumns there is needed and what they should contatin
  displayedColumns: string[] = ['name', 'adress', 'total_Available_Lots','total_Occupied_Lots','basePrice','Edit','Delete'];
  // dataSource is what holds the data that displayedColumns shows
  dataSource = new MatTableDataSource<Parking_strutur>([]);
  // products holds the admins for when they should go over to dataSource
  products: any[] = [];

  @ViewChild(MatSort) sort!: MatSort;
  @ViewChild(MatPaginator) paginator!: MatPaginator;

  ngAfterViewInit() {
    this.dataSource.sort = this.sort;
    this.dataSource.paginator = this.paginator;
    this.loadParkingLotStrucurs();
  }

announceSortChange(sortState: Sort) {
    if (sortState.direction) {
      this._liveAnnouncer.announce(`Sorted ${sortState.direction}ending`);
    } else {
      this._liveAnnouncer.announce('Sorting cleared');
    }
  }

  // this makes a api call that get all of the Product information that we would want
  loadParkingLotStrucurs() {
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

Edit(element: any) {
    const dialogRef = this.dialog.open(EditDialog, {
      width: '400px', // adjust size
      data: element,   // pass the row data to the dialog
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        console.log(result);
        this.api.UpdateParking_Lot_Structur(result).subscribe(()=>{
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

openDetails(row: any){
  this.router.navigate(['/lots', row.parking_lot_Structur_ID]);
}

}
