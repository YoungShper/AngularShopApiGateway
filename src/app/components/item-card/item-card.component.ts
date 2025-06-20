import {Component, Inject, Input, OnInit} from '@angular/core';
import {ItemBase, Product, Article} from '../../shared/models/models';
import {NgClass, NgIf} from '@angular/common';
import {Router} from '@angular/router';
import {AuthService} from '../../services/auth-service';

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
  isCart: boolean = false;
  isAdmin: boolean = false;
  constructor(@Inject('item') public itemInjected: T, @Inject('align') public align: string, private router: Router, private auth: AuthService)
  {
    this.layout = align;
  }

  isProduct(item: ItemBase): item is Product {
    return 'price' in item;
  }

  goToEdit(item: ItemBase): any {
    this.router.navigate([item.className, item.id, 'edit']);
  }
  goToItem(item: ItemBase): void {

    this.router.navigate([item.className, item.id]);
  }

  ngOnInit(): void {
    this.item = this.item || this.itemInjected;
    this.isAdmin = this.auth.getUser()?.isAdmin ?? false;
  }

  increase(item: Product) {

  }

  decrease(item: T & Product) {

  }
}
