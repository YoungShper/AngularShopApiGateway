import {Component, EventEmitter, Output} from '@angular/core';
import {FormsModule} from '@angular/forms';

@Component({
  selector: 'app-searchbar',
  imports: [
    FormsModule
  ],
  templateUrl: './searchbar.component.html',
  styleUrl: './searchbar.component.scss'
})
export class SearchbarComponent {
  @Output() search = new EventEmitter<string>();
  searchQuery: string = '';

  emitSearch() {
    this.search.emit(this.searchQuery);
  }
}
