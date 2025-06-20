import {Component, Input} from '@angular/core';
import {NgIf} from '@angular/common';

@Component({
  selector: 'app-popup-window',
  imports: [
    NgIf
  ],
  templateUrl: './popup-window.component.html',
  styleUrl: './popup-window.component.scss'
})
export class PopupWindowComponent {

  @Input() message = '';
  visible = false;

  show(message: string) {
    this.message = message;
    this.visible = true;

    setTimeout(() => {
      this.visible = false;
    }, 3000);
  }
}
