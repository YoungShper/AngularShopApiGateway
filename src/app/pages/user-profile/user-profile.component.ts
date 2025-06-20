import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, ReactiveFormsModule, Validators} from '@angular/forms';
import {HttpService} from '../../services/http.service';
import {User} from '../../shared/models/models';
import {AuthService} from '../../services/auth-service';
import {NgIf} from '@angular/common';



@Component({
  selector: 'app-user-profile',
  imports: [
    ReactiveFormsModule,
    NgIf
  ],
  templateUrl: './user-profile.component.html',
  styleUrl: './user-profile.component.scss'
})
export class UserProfileComponent implements OnInit {
  userForm!: FormGroup;
  isLoading = true;
  private originalUser!: User;


  constructor(private fb: FormBuilder, private http: HttpService, private auth: AuthService) {}

  ngOnInit(): void {
    this.loadUser();
  }

  loadUser(): void {
    this.http.getDataByID<User>('users', this.auth.getUser()?.id || '').subscribe({
      next: (user) => {
        this.originalUser = user
        this.userForm = this.fb.group({
          id: [user.id],
          name: [user.name, Validators.required],
          lastName: [user.lastName, Validators.required],
          address: [user.address],
          postalCode: [user.postalCode],
          city: [user.city],
          mail: [user.mail, [Validators.required, Validators.email]]
        });
        this.isLoading = false;
      },
      error: () => {
        this.isLoading = false;
        alert('Не удалось загрузить данные пользователя');
      }
    });
  }

  onSubmit(): void {
    if (this.userForm.invalid) return;

    const updatedUser: User = {
      ...this.originalUser,
      ...this.userForm.value
    };

    this.http.updateData(`users`, updatedUser).subscribe({
      next: () => alert('Данные успешно обновлены'),
      error: () => alert('Ошибка при обновлении')
    });
  }

  changePassword() {

  }

  logout() {

  }
}
