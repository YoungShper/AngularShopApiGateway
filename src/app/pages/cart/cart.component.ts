import {Component, OnInit} from '@angular/core';
import {CurrencyPipe, NgForOf, NgIf} from '@angular/common';
import {FormsModule} from '@angular/forms';
import {CartDataModel, Product} from '../../shared/models/models';
import {VerticalListComponent} from '../../components/vertical-list/vertical-list.component';
import {ItemCardComponent} from '../../components/item-card/item-card.component';
import {HttpService} from '../../services/http.service';
import {CartItemComponent} from '../../components/cart-item/cart-item.component';
import {CartService} from '../../services/cart-service';

@Component({
  selector: 'app-cart',
  imports: [
    FormsModule,
    NgForOf,
    CartItemComponent
  ],
  templateUrl: './cart.component.html',
  styleUrl: './cart.component.scss'
})
export class CartComponent implements OnInit {
  cartItems: CartDataModel[] = [];
  constructor(private http: HttpService, private cart: CartService) {
  }
  ngOnInit(): void {
      this.cart.refreshActualCart().subscribe(products => {this.cartItems = products || [];});
  }

  public get actualCartItems() {
    return this.cartItems.filter(item => item.isActual === true);
  }

  public get unActualCartItems() {
    return this.cartItems.filter(item => item.isActual === false);
  }
}
