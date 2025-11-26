import { NgFor } from '@angular/common';
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { ParkingStrucursService } from '../../Services/parking-strucurs-service';

@Component({
  selector: 'app-parking-list-component',
  imports: [NgFor],
  templateUrl: './parking-list-component.html',
  styleUrl: './parking-list-component.css',
})
export class ParkingListComponent {
  parkingStructures: { parking_lot_Structur_ID: string, name: string }[] = [];

  constructor(private router: Router, private api: ParkingStrucursService) { }

  ngOnInit() {
    this.loadParkingLotStrucurs(); // Load parking structures when component is initialized
  }

  loadParkingLotStrucurs() {
    this.api.getparking_Lot_Structur().subscribe({
      next: (res) => {
        this.parkingStructures = res; // Assign the response to the parkingStructures array
      },
      error: (err) => console.error('API error:', err), // Log any errors
    });
  }

  // Method to handle the selection of a parking structure
  onSelectParkingStructure(parkingId: string) {
    const currentUrl = this.router.url;
    if (currentUrl === '/carin') {
      this.router.navigate(['/formin', parkingId]); // Navigate to the form with selected parking ID
    } else if (currentUrl === '/carout') {
      this.router.navigate(['/formout', parkingId]);
    }
  }
}
