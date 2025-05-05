import { Injectable } from '@angular/core';
import {} from '../shared/models/models'
import {Observable} from 'rxjs';
import {HttpClient} from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  constructor(private http: HttpClient)
  {

  }
}
