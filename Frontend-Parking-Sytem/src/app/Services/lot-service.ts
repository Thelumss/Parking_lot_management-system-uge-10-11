import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class LotService {

  private apiUrl = 'https://localhost:7057/Lot/';

  constructor(private http: HttpClient) {}

  getLots(id:number): Observable<any> {
      const headers = new HttpHeaders({
      'Authorization': `Bearer ${localStorage.getItem('JWT_Token')}`,
      'Content-Type': 'application/json'
    });
    return this.http.get(this.apiUrl+'byparking_Lot_Structur'+id,{headers});
  }

}
