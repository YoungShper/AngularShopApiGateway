import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {Product} from '../../shared/models/models';
import {HttpService} from '../../services/http.service';
import {CommonModule} from '@angular/common';

@Component({
  selector: 'app-product-detail',
  imports: [CommonModule],
  templateUrl: './product-detail.component.html',
  styleUrl: './product-detail.component.scss'
})
export class ProductDetailComponent implements OnInit {
  item!: Product;
  id!: string;

  constructor(private route: ActivatedRoute, private http: HttpService) { }

  ngOnInit(): void {
    this.id = this.route.snapshot.paramMap.get('id')!;

    this.http.getDataByID<Product>('products', this.id).subscribe(data => {
      this.item = data;
    });
  }

  addToCart(item: Product) {

  }
}
