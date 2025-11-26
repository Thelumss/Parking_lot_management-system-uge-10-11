import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatFormField, MatLabel } from '@angular/material/form-field';
import { ParkingStrucursService } from '../../Services/parking-strucurs-service';

@Component({
  selector: 'app-edit-dialog',
  standalone: true,
  imports: [MatFormField,MatLabel,ReactiveFormsModule],
  templateUrl: './edit-dialog.html',
  styleUrl: './edit-dialog.css',
})
export class EditDialog {
  form: FormGroup;

  constructor(
    private fb: FormBuilder,
    private parkingservice: ParkingStrucursService,
    public dialogRef: MatDialogRef<EditDialog>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {
    this.form = this.fb.group({
      parking_lot_Structur_ID: [data.parking_lot_Structur_ID],
      name: [data.name],
      adress: [data.adress],
      total_Available_Lots: [data.total_Available_Lots],
      total_Occupied_Lots: [data.total_Occupied_Lots],
      basePrice: [data.basePrice]
    });
  }

  save() {
    this.dialogRef.close(this.form.value);
  }

  close() {
    this.dialogRef.close();
  }
}