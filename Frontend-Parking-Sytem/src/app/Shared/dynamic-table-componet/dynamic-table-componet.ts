import { NgFor } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-dynamic-table-componet',
  imports: [NgFor, FormsModule],
  templateUrl: './dynamic-table-componet.html',
  styleUrl: './dynamic-table-componet.css',
})
export class DynamicTableComponet {
  constructor(private route: ActivatedRoute, private router: Router) {}

  @Input() columns: string[] = [];
  @Input() data: any[] = [];
  @Output() editRow = new EventEmitter<any>();  // Emit the row to parent for editing
  @Output() rowDoubleClicked = new EventEmitter<any>();

  // Pagination Variables
  currentPage: number = 1;
  pageSize: number = 10;

  // Sorting Variables
  sortColumn: string = '';
  sortDirection: 'asc' | 'desc' = 'asc';


  // Search Filter
  searchTerm: string = '';

  // Row Selection
  selectedRows: Set<number> = new Set();

  // Pagination Logic
  get paginatedData() {
    const start = (this.currentPage - 1) * this.pageSize;
    return this.filteredData.slice(start, start + this.pageSize);
  }

  // Filtered Data (search filter)
  get filteredData() {
    return this.data.filter((row) =>
      this.columns.some((col) =>
        row[col].toString().toLowerCase().includes(this.searchTerm.toLowerCase())
      )
    );
  }

  get totalPages() {
    return Math.ceil(this.filteredData.length / this.pageSize);
  }

  // Sort Logic
  sortData(column: string) {
    this.sortDirection = this.sortColumn === column && this.sortDirection === 'asc' ? 'desc' : 'asc';
    this.sortColumn = column;
    this.data.sort((a, b) => {
      const aVal = a[column];
      const bVal = b[column];

      if (aVal < bVal) return this.sortDirection === 'asc' ? -1 : 1;
      if (aVal > bVal) return this.sortDirection === 'asc' ? 1 : -1;
      return 0;
    });
  }

  // Row Selection Logic
  toggleRowSelection(rowIndex: number) {
    if (this.selectedRows.has(rowIndex)) {
      this.selectedRows.delete(rowIndex);
    } else {
      this.selectedRows.add(rowIndex);
    }
  }

  // Editable Cells Logic
  onEdit(row: any) {
    this.editRow.emit(row);  // Emit the row data to parent component
  }

  onRowDoubleClick(row: any) {
    this.rowDoubleClicked.emit(row);  // This emits the row data to the parent component
  }

  // Pagination Controls
  goToPage(page: number) {
    this.currentPage = page;
  }
}
