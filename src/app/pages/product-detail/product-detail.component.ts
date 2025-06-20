import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {CartDataModel, Product} from '../../shared/models/models';
import {HttpService} from '../../services/http.service';
import {CommonModule} from '@angular/common';
import {CartService} from '../../services/cart-service';

@Component({
  selector: 'app-product-detail',
  imports: [CommonModule],
  templateUrl: './product-detail.component.html',
  styleUrl: './product-detail.component.scss'
})
export class ProductDetailComponent implements OnInit {
  item!: Product;
  id!: string;

  constructor(private route: ActivatedRoute, private http: HttpService, private cart: CartService) { }

  ngOnInit(): void {
    this.id = this.route.snapshot.paramMap.get('id')!;

    this.http.getDataByID<Product>('products', this.id).subscribe(data => {
      this.item = data;
    });
  }

  isAddable(): boolean {
    let data = this.cart.getCart();
    if(data !== null && data.length > 0)
    {
      return !(data.filter(cart => cart.isActual &&
        cart.products.filter(prod => prod.id === this.item.id).length > 0).length > 0);
    }
    return true;
  }

  addToCart(item: Product) {
     this.cart.addToCart(item);
  }
}
