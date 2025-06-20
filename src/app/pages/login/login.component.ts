import {Component, ViewChild} from '@angular/core';
import {FormsModule} from '@angular/forms';
import {NgIf} from '@angular/common';
import {ActivatedRoute, Router} from '@angular/router';
import {HttpService} from '../../services/http.service';
import {PopupWindowComponent} from '../../components/popup-window/popup-window.component';

@Component({
  selector: 'app-login',
  imports: [
    FormsModule,
    NgIf,
    PopupWindowComponent
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
  loginSuccess = false;
  loginForm: any = {
    email: '',
    password: '',
  };
  @ViewChild(PopupWindowComponent) popup!: PopupWindowComponent;

  triggerPopup(msg:string) {
    this.popup.show(msg);
  }

  constructor(private http: HttpService, private route: ActivatedRoute, private router: Router){

  }
  register() {

  }

  login() {
    this.http.login(this.loginForm.email, this.loginForm.password).subscribe({
      next: (data) => {
        console.log('Ответ от сервера:', data);
        this.loginSuccess = data;
        if (this.loginSuccess) {
          this.router.navigate(['/']);
        } else {
          this.triggerPopup('неверный логин или пароль');
        }
      },
      error: (err) => {
        console.error(err);
        this.triggerPopup('ошибка при авторизации');
      }
    });
  }
}
