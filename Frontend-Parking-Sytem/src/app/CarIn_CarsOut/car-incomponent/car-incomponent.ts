import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-car-incomponent',
  imports: [FormsModule],
  templateUrl: './car-incomponent.html',
  styleUrl: './car-incomponent.css',
})
export class CarIncomponent {

  numberPlate: string = '';
  carType: string = 'standard'; // Default car type
  parkingStructureId: string = '';

  constructor(private route: ActivatedRoute) { }

  ngOnInit(): void {
    // Capture the parking structure ID from the URL parameter
    this.route.paramMap.subscribe(params => {
      this.parkingStructureId = params.get('id') || '';
    });
  }

  // Submit handler for the form
  onSubmit() {
    console.log('Number Plate:', this.numberPlate);
    console.log('Lot Type:', this.carType);
    console.log('Parking Structure ID:', this.parkingStructureId);
  }
}
