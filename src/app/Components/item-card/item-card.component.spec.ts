import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ItemCardComponent } from './item-card.component';
import {Product} from '../../shared/models/models';

describe('ProductCardComponent', () => {
  let component: ItemCardComponent<Product>;
  let fixture: ComponentFixture<ItemCardComponent<Product>>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ItemCardComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ItemCardComponent<Product>);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
