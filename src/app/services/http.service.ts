import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Article, ItemBase, Product} from '../shared/models/models';
import {map, Observable} from 'rxjs';
import {environment} from '../../enviroments/eviroment';

@Injectable({
  providedIn: 'root'
})
export class HttpService {

  private baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  getProducts() : Observable<Product[]> {

    return this.http.get<{ productList: Product[] }>(`${this.baseUrl}products/get`).pipe(
      map(response => response.productList));
  }

  getArticles() : Observable<Article[]>
  {
    return this.http.get<{ articleList: Product[] }>(`${this.baseUrl}articles/get`).pipe(
      map(response => response.articleList));
  }

}
