import { Injectable } from '@angular/core';
import { BehaviorSubject, catchError, map, Observable, of, tap } from 'rxjs';
import { HttpService } from './http.service';
import {CartDataModel, CartModel, Product} from '../shared/models/models';
import {AuthService} from './auth-service'; // Предполагается, что модель корзины определена здесь

@Injectable({ providedIn: 'root' })
export class CartService {
  private cartSubject = new BehaviorSubject<CartDataModel[] | null>(null);
  public cart$ = this.cartSubject.asObservable();

  constructor(private http: HttpService, private auth: AuthService) {
  }

  // Запрашивает корзину с сервера и обновляет состояние
  public refreshActualCart(): Observable<CartDataModel[] | null> {

    return this.http.getData<CartDataModel>('cart').pipe(
      tap(cart => {
        this.cartSubject.next(cart)
      }),
    );
  }

  // Получает текущее значение корзины
  getCart(): CartDataModel[] | null {
    return this.cartSubject.value;
  }

  getActualCartId(): Observable<string | undefined> {
    return this.refreshActualCart().pipe(
      map(carts => carts?.find(cart => cart.isActual)?.cartId)
    );
  }

  addToCart(item: Product): void {
    const userId = this.auth.getUser()?.id;

    if (!userId) {
      alert('необходима авторизация');
      return;
    }

    this.getActualCartId().subscribe(cartId => {
      const data: CartModel = {
        id: undefined,
        isActual: true,
        cartId: cartId,
        productId: item.id,
        quantity: item.cartQuantity ?? 1,
        userId: userId,
        className: 'cart',
      };

      this.http.createData('cart', data).subscribe(() => {
        this.refreshActualCart().subscribe(); // обновить после добавления
      });
    });
  }
}
