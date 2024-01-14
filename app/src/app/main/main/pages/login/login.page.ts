import { Component, OnInit } from '@angular/core';
import {
  AppState,
  AuthService,
  RedirectLoginOptions,
} from '@auth0/auth0-angular';
import { Router } from '@angular/router';
import { CoreFacade } from '../../../../core/state/core.facade';

@Component({
  selector: 'login',
  templateUrl: './login.page.html',
})
export class LoginPage implements OnInit {
  constructor(
    public readonly authService: AuthService,
    private readonly router: Router,
    private readonly coreFacade: CoreFacade
  ) {}

  getLoginOptions(): RedirectLoginOptions<AppState> {
    return {
      appState: { target: '/intro' },
    };
  }

  ngOnInit(): void {
    this.authService.isAuthenticated$.subscribe(isLoggedIn => {
      if (isLoggedIn) {
        this.router.navigate(['intro']);
      } else {
        this.coreFacade.setTenantId('');
        this.authService.loginWithRedirect(this.getLoginOptions());
      }
    });
  }
}
