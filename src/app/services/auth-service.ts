import {BehaviorSubject, catchError, map, Observable, of, tap} from 'rxjs';
import {Injectable} from '@angular/core';
import {HttpService} from './http.service';
import {AuthPayload} from '../shared/models/models';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private loggedIn = new BehaviorSubject<boolean>(false);
  public isLoggedIn$ = this.loggedIn.asObservable();

  constructor(private http: HttpService) {
  }

  private userSubject = new BehaviorSubject<AuthPayload | null>(null);
  user$ = this.userSubject.asObservable();

  checkLogin(): Observable<boolean> {
    return this.http.checkLogin().pipe(
      tap(payload => this.userSubject.next(payload)),
      map(() => true),
      catchError(() => {
        this.userSubject.next(null);
        return of(false);
      })
    );
  }
  getUser(): AuthPayload | null {
    return this.userSubject.value;
  }
  logout(): Observable<boolean> {
    return this.http.logout().pipe(
      tap(success => {
        if (success) {
          this.userSubject.next(null);
        }
      })
    );
  }
}
