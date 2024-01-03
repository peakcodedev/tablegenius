import { Injectable } from '@angular/core';
import { catchError, map, Observable, of, take } from 'rxjs';
import { AuthService } from '@auth0/auth0-angular';
import { jwtDecode } from 'jwt-decode';

@Injectable({
  providedIn: 'root',
})
export class AuthenticationService {
  constructor(private readonly authService: AuthService) {}

  private getDecodedAccessToken(): Observable<any> {
    return this.authService
      .getAccessTokenSilently()
      .pipe(map(token => jwtDecode(token)));
  }

  private getPermissions(): Observable<string[]> {
    return this.getDecodedAccessToken().pipe(
      map(token => token.permissions),
      catchError(_ => {
        return of([]);
      })
    );
  }

  isSuperAdmin(): Observable<boolean> {
    return this.getPermissions().pipe(
      map(permissions => permissions.includes('superadmin')),
      take(1)
    );
  }

  isAdmin(): Observable<boolean> {
    return this.getPermissions().pipe(
      map(permissions => permissions.includes('admin')),
      take(1)
    );
  }
}
