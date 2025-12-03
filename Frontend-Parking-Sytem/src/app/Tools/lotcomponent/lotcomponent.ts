import { Component, Input } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { LotService } from '../../../Services/lot-service';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatTableModule } from '@angular/material/table';
import { DynamicTableComponet } from "../../Shared/dynamic-table-componet/dynamic-table-componet";
import { EditDialog } from '../../edit-dialog/edit-dialog';
import { Parking_strutur } from '../parking-structurcomponent/parking-structurcomponent';
import { MatDialog } from '@angular/material/dialog';

export interface Lot {
  lotID: number;
  lotName: string;
  occupied_Status: boolean;
  structur_ID: number;
  lot_types_ID: number;
}
export interface Createlotdto {
  Areaname: string;
  amount: number;
  Structur_ID: number;
  lottypes: number;
}
export interface readlotDTO{
  lotName: string;
  occupiedStatus: boolean;
  parkingLotStructurName: string;
  lotTypeName: string;
}

@Component({
  selector: 'app-lotcomponent',
  standalone: true,
  imports: [MatTableModule, MatPaginatorModule, MatSortModule, MatFormFieldModule, DynamicTableComponet],
  templateUrl: './lotcomponent.html',
  styleUrl: './lotcomponent.css',
})
export class Lotcomponent {
  ParkingLotStrucurID: number = 0;

  displayedColumns = [
    { key: 'lotName', label: 'Lot name' },
    { key: 'occupiedStatus', label: 'Occupied' },
    { key: 'parkingLotStructurName', label: 'Structur name' },
    { key: 'lotTypeName', label: 'lot types name' },
  ];
  CreaeteLotsColumns = [
    { key: 'Areaname', label: 'Areaname/Floorname' },
    { key: 'amount', label: 'Amount' },
    { key: 'lot_types_ID', label: 'Lot types name', type:'select', options:[ {value: 1, label:"Standard"},{value: 2, label:"EV"},{value: 3, label:"Handicapped"}] },
  ];

  constructor(private api: LotService, private route: ActivatedRoute, private dialog: MatDialog) { }

  @Input() columns: string[] = [];
  @Input() data: Lot[] = [];


  ngAfterViewInit() {
    this.route.params.subscribe(params => {
      this.ParkingLotStrucurID = +params['id'];  // '+' converts the string to a number
      this.loadLotsParkingLotStrucur();
    });
  }

  loadLotsParkingLotStrucur() {
    this.api.getLots(this.ParkingLotStrucurID).subscribe({
      next: res => {
        this.data = res.map((lot: readlotDTO) =>{
          return {
            ...lot,
            occupiedStatus: lot.occupiedStatus ? 'Yes' : 'No',
          }
        });
      },
      error: err => console.error('API error:', err)
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
          Areaname: '',
          lotName: '',
          amount: 0,
          structur_ID: this.ParkingLotStrucurID,
          lot_types_ID: 0
        },
        columns: this.CreaeteLotsColumns,
        title: 'Create',
        isNew: true
      },
      panelClass: 'custom-dialog-container'
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        const lotdto: Createlotdto= {
          Areaname: result.Areaname,
          amount: result.amount,
          Structur_ID: this.ParkingLotStrucurID,
          lottypes: result.lot_types_ID

        };

        this.api.Create_Lots(lotdto).subscribe(() => {
        this.loadLotsParkingLotStrucur();
        });
      }
    });
  }
}
