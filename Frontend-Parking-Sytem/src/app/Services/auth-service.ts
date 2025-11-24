import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, catchError, map, of } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AuthService {

  private apiUrl = 'https://localhost:7057/api/Auth/';

  private isLoggedIn = new BehaviorSubject<boolean>(false);
  loggedIn = this.isLoggedIn.asObservable();
  
  constructor(private http: HttpClient) {
    this.checkToken();
  }

  private checkToken() {
    if (typeof localStorage !== 'undefined') {
      const token = localStorage.getItem('JWT_Token');
      this.isLoggedIn.next(!!token); // true if token exists
    }
  }

    login(userDetails: { username: string, password: string }) {
    return this.http.post<any>(this.apiUrl+'login', userDetails)
      .pipe(
        map(response => {
          localStorage.setItem('JWT_Token', response.access_token);
          this.setData(true);
          return true;
        }),
        catchError(error => {
          console.log(error);
          this.setData(false);
          return of(false);
        })
      );
  }

  logout(): void {
    if (typeof localStorage !== 'undefined') {
      localStorage.removeItem('JWT_Token');
    }
    this.isLoggedIn.next(false);
  }

  setData(data: boolean) {
    this.isLoggedIn.next(data);
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
