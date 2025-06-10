import {Component, OnInit} from '@angular/core';
import {CarouselComponent} from '../../components/carousel/carousel.component';
import {ItemCardComponent} from '../../components/item-card/item-card.component';
import {Article, CategoryModel, PagedData, Product} from '../../shared/models/models';
import {HttpService} from '../../services/http.service';
import {VerticalListComponent} from '../../components/vertical-list/vertical-list.component';
import {PaginationComponent} from '../../components/pagination/pagination.component';
import {SearchbarComponent} from '../../components/searchbar/searchbar.component';
import {FormsModule} from '@angular/forms';
import {CommonModule} from '@angular/common';
import {ActivatedRoute, Router} from '@angular/router';
@Component({
  selector: 'app-catalog',
  imports: [
    VerticalListComponent,
    PaginationComponent,
    SearchbarComponent,
    FormsModule,
    CommonModule
  ],
  templateUrl: './catalog.component.html',
  styleUrl: './catalog.component.scss'
})
export class CatalogComponent implements OnInit {
  private searchQuery: string | null = null;
  constructor(private http: HttpService, private route: ActivatedRoute, private router: Router) {
  }
  public products!: Product[];
  public pagedItems!: PagedData<Product>;
  public priceFrom : number | null = null;
  public priceTo  : number | null = null;
  public page : number = 1;
  public totalPages!: number;
  public categories! : CategoryModel[];
  selectedCategories: { [id: string]: boolean } = {};

  protected readonly itemCardComponent = ItemCardComponent;

  ngOnInit()
  {
    this.http.getData<CategoryModel>('categories').subscribe(prod =>  this.categories = prod);

    this.route.queryParams.subscribe(params => {

      this.searchQuery = params['search'] || '';
      this.priceFrom = +params['priceFrom'] || null;
      this.priceTo = +params['priceTo'] || null;
      this.page = +params['page'] || 1;

      const cats: string[] = params['categories']?.split(',') || null;
      cats.forEach(id => this.selectedCategories[id] = true);
    });

    this.applyFilters();
  }

  applyFilters(): void {

    const selectedCategoryIds = Object.entries(this.selectedCategories)
      .filter(([_, checked]) => checked === true)
      .map(([id]) => id);

    const queryParams: any = {
      search: this.searchQuery || null,
      priceFrom: this.priceFrom || null,
      priceTo: this.priceTo || null,
      categories: selectedCategoryIds.length > 0 ? selectedCategoryIds.join(',') : null,
      page: this.page || 1,
    };
    this.router.navigate([], {
      queryParams: queryParams,
      queryParamsHandling: 'merge',
    });

    this.loadFilteredProducts(queryParams);
  }

  onFiltersChanged(){
    this.page = 1;
    this.applyFilters()
  }

  onSearch(query: any) {
    this.searchQuery = query;
    this.page = 1;
    this.applyFilters();
  }
  onPageChanged($event: number) {
    this.page = $event;
    this.applyFilters();
  }

  private loadFilteredProducts(params: any) {
    this.http.getPagedData<Product>('products', params).subscribe(prod =>  {
      this.pagedItems = prod;
      this.products = prod.items;
      this.totalPages = prod.totalPages;
    });
  }
}
