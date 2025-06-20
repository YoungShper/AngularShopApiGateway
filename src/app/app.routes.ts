import { Routes } from '@angular/router';
import { CartComponent} from './pages/cart/cart.component';
import {HomeComponent} from './pages/home/home.component';
import {LoginComponent} from './pages/login/login.component';
import {CatalogComponent} from './pages/catalog/catalog.component';
import {ProductDetailComponent} from './pages/product-detail/product-detail.component';
import {ArticleDetailComponent} from './components/article-detail/article-detail.component';
import {UserProfileComponent} from './pages/user-profile/user-profile.component';
import {NutritionRecommendationComponent} from './pages/nutrition-recommendation/nutrition-recommendation.component';
import {ProductEditComponent} from './pages/product-edit/product-edit.component';

export const routes: Routes = [
  { path: 'cart', component: CartComponent },
  { path: '', component: HomeComponent},
  { path: 'catalog', component: CatalogComponent},
  { path: 'account', component: UserProfileComponent},
  { path: 'login', component: LoginComponent},
  { path: 'products/:id', component: ProductDetailComponent },
  { path: 'article/:id', component: ArticleDetailComponent },
  { path: 'recommendations', component: NutritionRecommendationComponent },
  { path: 'products/:id/:action', component: ProductEditComponent },
];
