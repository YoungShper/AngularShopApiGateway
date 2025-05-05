import {
  Component,
  CUSTOM_ELEMENTS_SCHEMA,
  Injector,
  Input,
  Type,
} from '@angular/core';
import {NgComponentOutlet, NgForOf} from '@angular/common';
import {SwiperOptions} from 'swiper/types';


@Component({
  selector: 'app-carousel',
  imports: [
    NgForOf,
    NgComponentOutlet
  ],
  templateUrl: './carousel.component.html',
  styleUrl: './carousel.component.scss',
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class CarouselComponent
{

  @Input() items: any[] = [];
  @Input() itemComponent!: Type<any>;
  @Input() carouselHeader!: string;

  constructor(private injector: Injector) {
  }

  createInjector(item: any): Injector {
    return Injector.create({
      providers: [
        {provide: 'item', useValue: item}
      ],
      parent: this.injector
    });
  }


  breakpoints = {
    100: {
      slidesPerView: 1,
      spaceBetween: 5,
    },
    420: {
      slidesPerView: 2,
      spaceBetween: 5,
    },
    640: {
      slidesPerView: 3,
      spaceBetween: 5,
    },
    768: {
      slidesPerView: 4,
      spaceBetween: 5,
    },
    1024: {
      slidesPerView: 6,
      spaceBetween: 5,
    },
    1920: {
      slidesPerView: 8,
      spaceBetween: 5,
    },
  }

}
