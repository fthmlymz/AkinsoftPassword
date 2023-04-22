import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {MyPasswordModel} from "../../models/MyPasswordModel";
import {catchError, Observable, retry, throwError} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class MypasswordsService {

  constructor(private httpClient: HttpClient) { }

  getMyPasswords() {
    return this.httpClient
      .get<{data: MyPasswordModel[]}>(`https://localhost:2000/api/MyPasswords/GetPasswords`,
        {
          headers: new HttpHeaders().set('Content-Type', 'application/json; charset=utf-8')
        })
      .pipe(retry(3), catchError(this.handleError));
  }


  postPassword(data: any): Observable<MyPasswordModel> {
    return this.httpClient
      .post<any>(`https://localhost:2000/api/MyPasswords/CreatePassword`, data)
      .pipe(retry(3), catchError(this.handleError));
  }


  private handleError(error: any) {
    console.log(error.message)
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
