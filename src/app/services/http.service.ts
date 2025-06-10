import { Injectable } from '@angular/core';
import {HttpClient, HttpParams} from '@angular/common/http';
import {Article, ItemBase, PagedData, Product} from '../shared/models/models';
import {map, Observable} from 'rxjs';
import {environment} from '../../enviroments/eviroment';

@Injectable({
  providedIn: 'root'
})
export class HttpService {

  private apiUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }



  getData<T>(className: string, params?: {[key: string]: any}): Observable<T[]> {
    const url = `${this.apiUrl}${className}/`;
    let httpParams = new HttpParams();

    if (params) {
      Object.keys(params).forEach(key => {
        const value = params[key];
        // Пропускаем null и undefined
        if (value !== null && value !== undefined) {
          httpParams = httpParams.set(key, value.toString());
        }
      });
    }
    return this.http.get<T[]>(url, { params: httpParams });
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

    return this.http.get<PagedData<T>>(url, { params: httpParams });
  }

  getDataByID<T extends ItemBase>(className: string, id:string): Observable<T> {
    const url = `${this.apiUrl}${className}/${id}`;
    return this.http.get<any>(url).pipe(map(json => json as T));
  }

  createData<T extends ItemBase>(item: T): Observable<boolean> {
    const url = `${this.apiUrl}${item.className}/`;
    return this.http.post<any>(url, item).pipe(map(json => json[`message`] as boolean));
  }
    updateData<T extends ItemBase>(item: T): Observable<boolean> {
    const url = `${this.apiUrl}${item.className}/`;
    return this.http.put<any>(url, item).pipe(map(json => json[`message`] as boolean));
  }
  deleteData<T extends ItemBase>(item: T): Observable<boolean> {
    const url = `${this.apiUrl}${item.className}/${item.id}`;
    return this.http.delete<any>(url).pipe(map(json => json[`message`] as boolean));
  }

}
