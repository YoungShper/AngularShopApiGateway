import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NutritionRecommendationComponent } from './nutrition-recommendation.component';

describe('NutritionRecommendationComponent', () => {
  let component: NutritionRecommendationComponent;
  let fixture: ComponentFixture<NutritionRecommendationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [NutritionRecommendationComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(NutritionRecommendationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
