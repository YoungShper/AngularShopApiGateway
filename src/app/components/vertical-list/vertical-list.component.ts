import {Component, Injector, Input, OnInit, Type} from '@angular/core';
import {NgComponentOutlet, NgForOf} from '@angular/common';
import {AuthService} from '../../services/auth-service';

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
export class VerticalListComponent  {
  @Input() items: any[] = [];
  @Input() itemComponent!: Type<any>;
  @Input() carouselHeader!: string;
  isAdmin: boolean = false;

  constructor(private injector: Injector, private auth: AuthService) {
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
