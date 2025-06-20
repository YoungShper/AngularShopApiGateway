import {Component, OnInit} from '@angular/core';
import {NavigationEnd, Router, RouterLink} from '@angular/router';
import {NgOptimizedImage} from '@angular/common';
import {AuthService} from '../../services/auth-service';
import {filter} from 'rxjs';
import {CartService} from '../../services/cart-service';

@Component({
  selector: 'app-header',
  imports: [
    RouterLink,
    NgOptimizedImage
  ],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent implements OnInit {
  isLoggedIn = false;

  constructor(private authService: AuthService, private router: Router, private cart: CartService) {
  }

  ngOnInit(): void {
    // 1. Выполняем логику при первой загрузке страницы
    this.checkAndRefresh();

    // 2. Выполняем логику при каждом переходе по маршрутам внутри SPA
    this.router.events.pipe(
      filter(event => event instanceof NavigationEnd)
    ).subscribe(() => {
      this.checkAndRefresh();
    });
  }

  private checkAndRefresh(): void {
    this.authService.checkLogin().subscribe(isAuth => {
      this.isLoggedIn = isAuth;
      if (isAuth) {
        this.cart.refreshActualCart().subscribe(); // Не забудь подписаться
      }
    });
  }
}
