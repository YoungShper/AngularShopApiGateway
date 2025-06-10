import {Component, Inject, Input, OnInit} from '@angular/core';
import {ItemBase, Product, Article} from '../../shared/models/models';
import {NgClass, NgIf} from '@angular/common';
import {Router} from '@angular/router';

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
  layout: string;
  constructor(@Inject('item') public itemInjected: T, @Inject('align') public align: string, private router: Router)
  {
    this.layout = align;
  }

  isProduct(item: ItemBase): item is Product {
    return 'price' in item;
  }
  goToItem(item: ItemBase): void {

    let queryParams: any = {};
    this.router.navigate([item.className, item.id]);
  }

  ngOnInit(): void {
    this.item = this.item || this.itemInjected;
  }
}
