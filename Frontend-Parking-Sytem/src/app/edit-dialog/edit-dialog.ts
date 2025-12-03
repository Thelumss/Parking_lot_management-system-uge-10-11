import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule, NgFor, NgForOf, NgIf } from '@angular/common';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';

@Component({
  selector: 'app-edit-dialog',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatDialogModule,
    NgIf,
    NgFor,
    MatSelectModule,
  ],
  templateUrl: './edit-dialog.html',
  styleUrls: ['./edit-dialog.css'],
})
export class EditDialog {

  form: FormGroup;
  entries: { 
  key: string; 
  label: string; 
  value: any; 
  readonly: boolean; 
  type?: string; 
  options?: {value:any; label:string}[] 
}[] = [];

  editableEntries: { key: string; label: string; value: any }[] = [];
  readonlyKeys = ['total_Available_Lots', 'total_Occupied_Lots'];

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<EditDialog>,
    @Inject(MAT_DIALOG_DATA) public data: { row: any; columns: any[],title: string }
  ) {
    const row = data.row;
    const columns = data.columns;

    // Build entries with readonly info
    this.entries = columns.map(col => ({
      key: col.key,
      label: col.label,
      value: row[col.key],
      readonly: this.readonlyKeys.includes(col.key),
      type: col.type,
      options: col.options ?? []
    }));

    this.editableEntries = this.entries.filter(e => !e.readonly);
    // Build form controls for editable fields only
    const controls: any = {};
    this.entries.forEach(e => {
      if (!e.readonly) {
        controls[e.key] = this.fb.control(e.value); // default value = current value
      }
    });

    this.form = this.fb.group(controls);
  }

  save() {
    if (this.form.valid) {
      // Combine readonly values with form values before closing
      const result = { ...this.data.row, ...this.form.value };
      this.dialogRef.close(result);
    }
  }

  cancel() {
    this.dialogRef.close(null);
  }
}
