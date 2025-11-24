import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ParkingStrucursService {
  
  private apiUrl = 'https://localhost:7057/parking_Lot_Structur/byOrganisation';

  constructor(private http: HttpClient) {}

  getparking_Lot_Structur(): Observable<any> {
      const headers = new HttpHeaders({
      'Authorization': `Bearer ${localStorage.getItem('JWT_Token')}`,
      'Content-Type': 'application/json'
    });
    return this.http.get(this.apiUrl,{headers});
  }

}
