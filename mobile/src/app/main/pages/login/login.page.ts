import { Component, OnInit } from '@angular/core';
import { AuthService } from '@auth0/auth0-angular';
import { Router } from '@angular/router';
import { Browser } from '@capacitor/browser';

@Component({
  selector: 'login',
  templateUrl: './login.page.html',
})
export class LoginPage implements OnInit {
  constructor(
    public readonly authService: AuthService,
    private readonly router: Router
  ) {}

  ngOnInit(): void {
    this.authService.isAuthenticated$.subscribe(isLoggedIn => {
      console.error(isLoggedIn);
      if (isLoggedIn) {
        this.router.navigate(['intro']);
      } else {
        this.authService
          .loginWithRedirect({
            async openUrl(url: string) {
              await Browser.open({ url, windowName: '_self' });
            },
            appState: {
              target: '/intro',
            },
          })
          .subscribe();
      }
    });
  }
}
