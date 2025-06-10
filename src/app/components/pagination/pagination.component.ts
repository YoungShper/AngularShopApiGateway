import {Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges} from '@angular/core';
import {NgClass, NgForOf} from '@angular/common';

@Component({
  selector: 'app-pagination',
  imports: [
    NgClass,
    NgForOf
  ],
  templateUrl: './pagination.component.html',
  styleUrl: './pagination.component.scss'
})
export class PaginationComponent implements OnInit, OnChanges {
  ngOnChanges(changes: SimpleChanges): void {
    if (changes['totalPages']) {
      this.totalPagesArray = Array.from({ length: this.totalPages }, (_, i) => i + 1);
    }
  }
  ngOnInit(): void {

  }
  @Input() currentPage!: number;
  @Input() totalPages!: number;
  @Output() currentPageEmitter = new EventEmitter<number>();

  totalPagesArray: number[] = [];

  goToPage(page: number) {
    this.currentPage = page;
    this.currentPageEmitter.emit(page);
  }
}
