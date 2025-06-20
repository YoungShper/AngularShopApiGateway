import { Component } from '@angular/core';
import {HttpClient, HttpParams} from '@angular/common/http';
import {HttpService} from '../../services/http.service';
import {Product, UserProfile} from '../../shared/models/models';
import {FormsModule} from '@angular/forms';
import {NgForOf, NgIf} from '@angular/common';
import {CarouselComponent} from '../../components/carousel/carousel.component';
import {ItemCardComponent} from '../../components/item-card/item-card.component'; // путь поправь

@Component({
  selector: 'app-nutrition-recommendation',
  templateUrl: './nutrition-recommendation.component.html',
  imports: [
    FormsModule,
    NgForOf,
    CarouselComponent,
    NgIf
  ],
  styleUrls: ['./nutrition-recommendation.component.scss']
})
export class NutritionRecommendationComponent {
  userProfile: UserProfile = {
    goal: 'maintain',
    weight: 70,
    height: 175,
    age: 25
  };

  goals = [
    { value: 'gain', display: 'Набрать массу, высокая физическая активность' },
    { value: 'loss', display: 'Похудеть, средняя физическая активность' },
    { value: 'maintain', display: 'Поддерживать вес, низкая физическая активность' }
  ];

  recommendations: Product[] = [];
  loading = false;
  errorMessage = '';

  constructor(private http: HttpService) {}

  onSubmit() {
    this.loading = true;
    this.errorMessage = '';
    this.recommendations = [];

    this.http.getRecommendations(
      this.userProfile.goal,
      this.userProfile.weight,
      this.userProfile.height,
      this.userProfile.age
    ).subscribe({
      next: (data) => {
        this.recommendations = data;
        this.loading = false;
      },
      error: (err) => {
        this.errorMessage = 'Ошибка при получении рекомендаций.';
        this.loading = false;
      }
    });
  }

  protected readonly itemCardComponent = ItemCardComponent;
}
