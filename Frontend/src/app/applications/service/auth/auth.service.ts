import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {catchError, Observable, retry, throwError} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private httpClient: HttpClient) { }

  postLogin(data: any): Observable<any> {
    return this.httpClient
      .post<any>("https://localhost:2000/api/Auth/Login", data, {
        headers: new HttpHeaders().set('Content-Type', 'application/json')
      })
      .pipe(retry(3), catchError(this.handleError));
  }
  registerUser(data: any): Observable<any>{
    return this.httpClient
      .post<any>("https://localhost:2000/api/Auth/Register", data, {
        headers: new HttpHeaders().set('Content-Type', 'application/json')
      })
      .pipe(retry(3), catchError(this.handleError))
  }

  private handleError(error: any) {
    let errorMessage = '';
    if (error.errorMessage instanceof ErrorEvent) {
      errorMessage = error.error.errorMessage;
    } else {
      errorMessage = `\nStatusCode: ${error.status}}\nResponse: ${error.message}`;
    }
    //window.alert(errorMessage);
    return throwError(() => {
      return errorMessage;
    });
  }

}
