import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class UsersService {
  
  private apiUrl = 'https://localhost:7057/user/';

  constructor(private http: HttpClient) {}

  getMeProfile(): Observable<any> {
      const headers = new HttpHeaders({
      'Authorization': `Bearer ${localStorage.getItem('JWT_Token')}`,
      'Content-Type': 'application/json'
    });
    return this.http.get(this.apiUrl+"getMe",{headers});
  }
    getUsersbyOrganisation(): Observable<any> {
      const headers = new HttpHeaders({
      'Authorization': `Bearer ${localStorage.getItem('JWT_Token')}`,
      'Content-Type': 'application/json'
    });
    return this.http.get(this.apiUrl+"byOrganisation",{headers});
  }
}
