import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class CarsService {
  private apiUrl = 'https://localhost:7057/Cars/';

  constructor(private http: HttpClient) {}

  carsin(Parking_Structure_ID: number,Number_Plate:string,Lot_Type:number): Observable<any> {
      const headers = new HttpHeaders({
      'Authorization': `Bearer ${localStorage.getItem('JWT_Token')}`,
      'Content-Type': 'application/json'
    });
    return this.http.put(this.apiUrl+'CarsIn/'+Parking_Structure_ID+"/"+Number_Plate+"/"+Lot_Type,{},{headers});
  }
  carsout(Parking_Structure_ID: number,Number_Plate:string): Observable<any> {
      const headers = new HttpHeaders({
      'Authorization': `Bearer ${localStorage.getItem('JWT_Token')}`,
      'Content-Type': 'application/json'
    });
    return this.http.put(this.apiUrl+'CarsOut/'+Parking_Structure_ID+"/"+Number_Plate,{},{headers});
  }

}
