import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, map, Observable, of } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ParkingStrucursService {

  private apiUrl = 'https://localhost:7057/parking_Lot_Structur/';

  constructor(private http: HttpClient) { }

  getparking_Lot_Structur(): Observable<any> {
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${localStorage.getItem('JWT_Token')}`,
      'Content-Type': 'application/json'
    });
    return this.http.get(this.apiUrl + 'byOrganisation', { headers });
  }

  DeleteParking_Lot_Structur(id: number): Observable<any> {
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${localStorage.getItem('JWT_Token')}`,
      'Content-Type': 'application/json'
    });
    return this.http.delete(this.apiUrl + 'delete' + id, { headers });
  }

  UpdateParking_Lot_Structur(parking_Lot_Structur: { parking_lot_Structur_ID: number, name: string, adress: string, total_Available_Lots: number, total_Occupied_Lots: number, basePrice: number }): Observable<any> {
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${localStorage.getItem('JWT_Token')}`,
      'Content-Type': 'application/json'
    });
    return this.http.put<any>(this.apiUrl + 'UpdateParking_Lot_Structur', parking_Lot_Structur, { headers })
      .pipe(
        map(response => {
          return response;
        }),
        catchError(error => {
          console.log(error);
          return of(false);
        })
      );
  }
  CreateParking_Lot_Structur(parking_Lot_Structur: { parking_lot_Structur_ID: number, name: string, adress: string, total_Available_Lots: number, total_Occupied_Lots: number, basePrice: number }) {
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${localStorage.getItem('JWT_Token')}`,
      'Content-Type': 'application/json'
    });
    return this.http.post<any>(this.apiUrl + 'CreateParking_Lot_Structur', parking_Lot_Structur, { headers })
      .pipe(
        map(response => {
          return response;
        }),
        catchError(error => {
          console.log(error);
          return of(false);
        })
      );
  }

}



