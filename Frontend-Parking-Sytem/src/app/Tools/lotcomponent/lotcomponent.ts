import { Component, inject, Input, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { LotService } from '../../../Services/lot-service';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInput } from '@angular/material/input';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatSort, MatSortModule, Sort } from '@angular/material/sort';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { LiveAnnouncer } from '@angular/cdk/a11y';
import { DynamicTableComponet } from "../../Shared/dynamic-table-componet/dynamic-table-componet";

export interface Lot {
  lotID: number;
  lotName: string;
  occupied_Status: boolean;
  structur_ID: number;
  lot_types_ID: number;
}

@Component({
  selector: 'app-lotcomponent',
  standalone: true,
  imports: [MatTableModule, MatPaginatorModule, MatSortModule, MatInput, MatFormFieldModule, DynamicTableComponet],
  templateUrl: './lotcomponent.html',
  styleUrl: './lotcomponent.css',
})
export class Lotcomponent {
   lotId: number = 0;

  constructor(private api: LotService, private route: ActivatedRoute) { }

  @Input() columns: string[] = [];
  @Input() data: Lot[] = [];


  ngAfterViewInit() {
    this.route.params.subscribe(params => {
      this.lotId = +params['id'];  // '+' converts the string to a number
      this.loadParkingLotStrucurs();
    });
  }

  loadParkingLotStrucurs() {
    this.api.getLots(this.lotId).subscribe({
      next: res => {
        this.data = res;
      },
      error: err => console.error('API error:', err)
    });
  }
}
