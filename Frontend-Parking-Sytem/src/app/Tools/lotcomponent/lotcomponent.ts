import { Component, inject, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { LotService } from '../../Services/lot-service';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInput } from '@angular/material/input';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatSort, MatSortModule, Sort } from '@angular/material/sort';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { LiveAnnouncer } from '@angular/cdk/a11y';

export interface Parking_strutur {
  parking_lot_Structur_ID: number;
  name: string;
  adress: string;
  total_Available_Lots: number;
  total_Occupied_Lots: number;
  basePrice: number;
}

@Component({
  selector: 'app-lotcomponent',
  standalone: true,
  imports: [MatTableModule, MatPaginatorModule,MatSortModule,MatInput,MatFormFieldModule],
  templateUrl: './lotcomponent.html',
  styleUrl: './lotcomponent.css',
})
export class Lotcomponent implements OnInit{

  parking: any;

    applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  private _liveAnnouncer = inject(LiveAnnouncer);
  // displayedColumns sets up how many collumns there is needed and what they should contatin
  displayedColumns: string[] = ['name', 'adress', 'total_Available_Lots','total_Occupied_Lots','basePrice','Edit','Delete'];
  // dataSource is what holds the data that displayedColumns shows
  dataSource = new MatTableDataSource<Parking_strutur>([]);
  // products holds the admins for when they should go over to dataSource
  products: any[] = [];

  @ViewChild(MatSort) sort!: MatSort;
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  
  constructor(
    private router: ActivatedRoute,
    private lot: LotService
  ) {

  }

    ngAfterViewInit() {
    this.dataSource.sort = this.sort;
    this.dataSource.paginator = this.paginator;
    this.loadlots();
  }

  loadlots() {
    this.lot.getLots(1).subscribe({
      next: res => {
        this.products = res;
        this.dataSource.data = this.products;
        this.dataSource.sort = this.sort;
        this.dataSource.paginator = this.paginator;
      },
      error: err => console.error('API error:', err)
    });
  }

  announceSortChange(sortState: Sort) {
    if (sortState.direction) {
      this._liveAnnouncer.announce(`Sorted ${sortState.direction}ending`);
    } else {
      this._liveAnnouncer.announce('Sorting cleared');
    }
  }

  ngOnInit(): void {
    const id = this.router.snapshot.paramMap.get('id');
    console.log(id);
  }



}
