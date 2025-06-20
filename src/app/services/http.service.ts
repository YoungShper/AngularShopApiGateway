import { Injectable } from '@angular/core';
import {HttpClient, HttpParams} from '@angular/common/http';
import {Article, AuthPayload, CartDataModel, ItemBase, PagedData, Product, User} from '../shared/models/models';
import {catchError, map, Observable, of, tap} from 'rxjs';
import {environment} from '../../enviroments/eviroment';
import {FormGroup} from '@angular/forms';

@Injectable({
  providedIn: 'root'
})
export class HttpService {

  private apiUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }



  getData<T>(className: string, params?: {[key: string]: any}): Observable<T[]> {
    const url = `${this.apiUrl}${className}`;
    let httpParams = new HttpParams();

    if (params) {
      Object.keys(params).forEach(key => {
        const value = params[key];
        if (value !== null && value !== undefined) {
          httpParams = httpParams.set(key, value.toString());
        }
      });
    }
    let result = this.http.get<T[]>(url, { params: httpParams, withCredentials: true });
    return result;
  }

  getPagedData<T extends ItemBase>(className: string, params?: { [key: string]: any }): Observable<PagedData<T>> {
    const url = `${this.apiUrl}${className}/`;
    let httpParams = new HttpParams();

    if (params) {
      Object.keys(params).forEach(key => {
        const value = params[key];
        if (value !== null && value !== undefined) {
          httpParams = httpParams.set(key, value.toString());
        }
      });
    }

    return this.http.get<PagedData<T>>(url, { params: httpParams, withCredentials: true });
  }

  getDataByID<T extends ItemBase>(className: string, id:string): Observable<T> {
    const url = `${this.apiUrl}${className}/${id}`;
    return this.http.get<any>(url, {withCredentials: true}).pipe(map(json => json as T));
  }

  createData<T>(className: string, data: T): Observable<boolean> {
    const url = `${this.apiUrl}${className}/`;
    return this.http.post<any>(url, data, {withCredentials: true}).pipe(map(json => json[`message`] as boolean));
  }
    updateData<T>(className: string, data: T): Observable<boolean> {
    const url = `${this.apiUrl}${className}`;
    return this.http.put<any>(url, data, {withCredentials: true,  headers: {
        'Content-Type': 'application/json'
      }}).pipe(map(json => json[`message`] as boolean));
  }
  deleteData(className: string, id:string): Observable<boolean> {
    const url = `${this.apiUrl}${className}/${id}`;
    return this.http.delete<any>(url, {withCredentials: true}).pipe(map(json => json[`message`] as boolean));
  }

  login(email: string, password:string): Observable<boolean> {

    const params = new HttpParams()
      .set('mail', email)
      .set('password', password);
    const url = `${this.apiUrl}users/login`;
    return this.http.get<any>(url, {params: params, withCredentials: true}).pipe(map(json => json[`message`] as boolean)
    );
  }

  checkLogin(): Observable<AuthPayload> {
    return this.http.get<AuthPayload>(`${this.apiUrl}users/check-auth`, {
      withCredentials: true
    });
  }

  logout(): Observable<boolean> {
    return this.http.get<{ message: boolean }>(`${this.apiUrl}users/logout`, {
      withCredentials: true
    }).pipe(
      map(response => response.message === true),
      catchError(() => of(false))
    );
  }

  getRecommendations(goal: string, weight: number, height: number, age: number): Observable<Product[]> {
    const url = `${this.apiUrl}products/recommend`;
    let params = new HttpParams()
      .set('goal', goal)
      .set('weight', weight.toString())
      .set('height', height.toString())
      .set('age', age.toString());

    return this.http.get<Product[]>(url, {
      params: params,
      withCredentials: true
    });
  }
}
