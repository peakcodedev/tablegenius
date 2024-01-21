import { Component } from '@angular/core';
import { AuthService } from '@auth0/auth0-angular';
import { Browser } from '@capacitor/browser';

const returnTo = `dev.peakcode.tablegenius://table-genius.eu.auth0.com/capacitor/dev.peakcode.tablegenius/loggedout`;

@Component({
  selector: 'app-logout-button',
  templateUrl: './logout-button.component.html',
  styleUrls: ['./logout-button.component.scss'],
})
export class LogoutButtonComponent {
  constructor(public auth: AuthService) {}

  logout() {
    this.auth
      .logout({
        logoutParams: {
          returnTo,
        },
        async openUrl(url: string) {
          await Browser.open({ url });
        },
      })
      .subscribe();
  }
}
