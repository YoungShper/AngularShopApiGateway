import {Component, Input} from '@angular/core';
import {Product} from '../../shared/models/models';
import {CurrencyPipe, NgIf} from '@angular/common';
import {VerticalListComponent} from '../vertical-list/vertical-list.component';
import {ItemCardComponent} from '../item-card/item-card.component';

@Component({
  selector: 'app-cart-item',
  imports: [
    CurrencyPipe,
    NgIf,
    VerticalListComponent
  ],
  templateUrl: './cart-item.component.html',
  styleUrl: './cart-item.component.scss'
})
export class CartItemComponent {
  @Input() public products!: Product[];
  @Input() public id!: string | undefined;
  @Input() public isActual!: boolean;

  protected readonly itemCardComponent = ItemCardComponent;

  getTotalPrice(): any {
    let result: number = 0;
    this.products.forEach(product => result += product.price);
    return result;
  }

  checkout() {


  }
}
