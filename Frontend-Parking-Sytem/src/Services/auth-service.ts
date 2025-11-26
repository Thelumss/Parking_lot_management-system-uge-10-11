import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, catchError, map, of } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AuthService {

  private apiUrl = 'https://localhost:7057/api/Auth/';

  loggedIn = new BehaviorSubject<boolean>(false);
  
  constructor(private http: HttpClient) {
    this.checkToken();
  }

  private checkToken() {
    if (typeof localStorage !== 'undefined') {
      const token = localStorage.getItem('JWT_Token');
      this.loggedIn.next(!!token); // true if token exists
    }
  }

    login(userDetails: { username: string, password: string }) {
    return this.http.post<any>(this.apiUrl+'login', userDetails)
      .pipe(
        map(response => {
          localStorage.setItem('JWT_Token', response.access_token);
          this.loggedIn.next(true);
          return true;
        }),
        catchError(error => {
          console.log(error);
          this.loggedIn.next(false);
          return of(false);
        })
      );
  }

  logout(): void {
    if (typeof localStorage !== 'undefined') {
      localStorage.removeItem('JWT_Token');
    }
    this.loggedIn.next(false);
  }

  getToken(): string | null {
    if (typeof localStorage !== 'undefined') {
      return localStorage.getItem('JWT_Token');
    }
    return null;
  }
  hasToken(): boolean {
  if (typeof window !== 'undefined' && window.localStorage) {
    return !!localStorage.getItem("JWT_Token");
  }
  return false; // fallback for server-side
}

  createUser(userDetails: { name: string, password: string ,email: string, userTypeID: number, organisationId: number}) {
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${localStorage.getItem('JWT_Token')}`,
      'Content-Type': 'application/json'
    });
      return this.http.post<any>(this.apiUrl+'register', userDetails, { headers})
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
