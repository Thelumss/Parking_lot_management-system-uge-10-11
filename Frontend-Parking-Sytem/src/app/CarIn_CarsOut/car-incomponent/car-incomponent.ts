import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { CarsService } from '../../../Services/cars-service';
import { NgIf } from '@angular/common';

@Component({
  selector: 'app-car-incomponent',
  standalone: true,
  imports: [FormsModule, NgIf],
  templateUrl: './car-incomponent.html',
  styleUrl: './car-incomponent.css',
})
export class CarIncomponent {

  numberPlate: string = '';
  carType: string = 'standard'; // Default car type
  parkingStructureId: string = '';

  showMessage = false;
  messageText = '';
  isSuccess = true;

  constructor(private route: ActivatedRoute, private carin: CarsService) { }

  ngOnInit(): void {
    // Capture the parking structure ID from the URL parameter
    this.route.paramMap.subscribe(params => {
      this.parkingStructureId = params.get('id') || '';
    });
  }

  // Submit handler for the form
  onSubmit() {
    this.carin.carsin(+this.parkingStructureId, this.numberPlate, +this.carType)
      .subscribe({
        next: (res) => {
          this.resetForm();
          this.showTempMessage('Car successfully checked In ✅ go to lot '+res.lotName, true);
        },
        error: () => {
          this.showTempMessage('Checkin failed ❌', false);
        }
      });
  }
  resetForm() {
    this.numberPlate = '';
    this.carType = 'standard'; // reset to default radio (Standard)
  }

  showTempMessage(text: string, success: boolean) {
    this.messageText = text;
    this.isSuccess = success;
    this.showMessage = true;

    setTimeout(() => {
      this.showMessage = false;
    }, 5000); // 5 seconds
  }
}
