import {Component, OnInit} from '@angular/core';
import {AsyncPipe, NgIf} from '@angular/common';
import {CarouselComponent} from '../../components/carousel/carousel.component';
import {HttpService} from '../../services/http.service';
import {Article, Product} from '../../shared/models/models';
import {ItemCardComponent} from '../../components/item-card/item-card.component';


@Component({
  selector: 'app-home',
  standalone: true,
  imports: [
    CarouselComponent,
    NgIf,
    AsyncPipe
  ],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss',
})
export class HomeComponent implements OnInit {

  public products!: Product[];
  public articles! : Article[];
  protected readonly itemCardComponent = ItemCardComponent;
  constructor(private http: HttpService) {
  }

  ngOnInit(): void {
        this.http.getData<Article>('articles').subscribe(articles => this.articles = articles);
    this.http.getData<Product>('products').subscribe(prod =>  this.products = prod);
  }
}
