import { Routes } from '@angular/router';
import { CartComponent} from './pages/cart/cart.component';
import {HomeComponent} from './pages/home/home.component';
import {LoginComponent} from './pages/login/login.component';
import {CatalogComponent} from './pages/catalog/catalog.component';

export const routes: Routes = [
  { path: 'cart', component: CartComponent },
  {path: '', component: HomeComponent},
  {path: 'catalog', component: CatalogComponent},
  {path: 'account', component: LoginComponent},
];
