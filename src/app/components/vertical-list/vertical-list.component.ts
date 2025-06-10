import {Component, Injector, Input, Type} from '@angular/core';
import {NgComponentOutlet, NgForOf} from '@angular/common';

@Component({
  selector: 'app-vertical-list',
  standalone: true,
  imports: [
    NgForOf,
    NgComponentOutlet
  ],
  templateUrl: './vertical-list.component.html',
  styleUrl: './vertical-list.component.scss'
})
export class VerticalListComponent {
  @Input() items: any[] = [];
  @Input() itemComponent!: Type<any>;
  @Input() carouselHeader!: string;

  constructor(private injector: Injector) {
  }

  createInjector(item: any): Injector {
    return Injector.create({
      providers: [
        {provide: 'item', useValue: item},
        {provide: 'align', useValue: 'horizontal'},
      ],
      parent: this.injector
    });
  }
}
