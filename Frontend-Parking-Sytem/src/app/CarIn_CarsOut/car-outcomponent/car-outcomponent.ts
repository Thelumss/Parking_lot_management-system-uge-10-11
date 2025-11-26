import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { CarsService } from '../../Services/cars-service';
import { NgIf } from '@angular/common';

@Component({
  selector: 'app-car-outcomponent',
  standalone: true,
  imports: [FormsModule,NgIf],
  templateUrl: './car-outcomponent.html',
  styleUrl: './car-outcomponent.css',
})
export class CarOutcomponent {
  numberPlate: string = '';
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
    this.carin.carsout(+this.parkingStructureId, this.numberPlate)
      .subscribe({
        next: () => {
          this.showTempMessage('Car successfully checked out ✅', true);
        },
        error: () => {
          this.showTempMessage('Checkout failed ❌', false);
        }
      });
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
