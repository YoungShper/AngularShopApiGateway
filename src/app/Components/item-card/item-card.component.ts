import {Component, Inject, Input, OnInit} from '@angular/core';
import {ItemBase, Product, Article} from '../../shared/models/models';
import {NgClass, NgIf} from '@angular/common';

@Component({
  selector: 'app-item-card',
  imports: [
    NgIf,
    NgClass
  ],
  templateUrl: './item-card.component.html',
  styleUrl: './item-card.component.scss'
})
export class ItemCardComponent<T extends ItemBase> implements OnInit {

  @Input() item!: T;
  @Input() baseUrl!: string;
  @Input() layout: 'vertical' | 'horizontal' = 'horizontal';
  constructor(@Inject('item') public itemInjected: T){}

  isProduct(item: ItemBase): item is Product {
    return 'price' in item;
  }
  goToItem(item: ItemBase): void {
    window.location.href = `${this.baseUrl}${item.className}/${item.id}`;
  }

  ngOnInit(): void {
    this.item = this.item || this.itemInjected;
  }
}
