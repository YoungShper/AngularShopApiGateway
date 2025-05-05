import {Component, OnInit} from '@angular/core';
import {NgComponentOutlet, NgForOf} from "@angular/common";
import {VerticalListComponent} from '../../components/vertical-list/vertical-list.component';
import {CarouselComponent} from '../../components/carousel/carousel.component';
import {ItemCardComponent} from '../../components/item-card/item-card.component';
import {Article, Product} from '../../shared/models/models';
import {HttpService} from '../../services/http.service';

@Component({
  selector: 'app-catalog',
  imports: [
    NgComponentOutlet,
    NgForOf,
    VerticalListComponent,
    CarouselComponent
  ],
  templateUrl: './catalog.component.html',
  styleUrl: './catalog.component.scss'
})
export class CatalogComponent implements OnInit {
  constructor(private http: HttpService) {
  }
  public products!: Product[];

  protected readonly itemCardComponent = ItemCardComponent;

  ngOnInit()
  {
    this.http.getProducts().subscribe(prod =>  this.products = prod);
  }
}
