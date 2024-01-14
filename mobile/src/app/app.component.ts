import { Component, OnInit, NgZone } from '@angular/core';
import { AuthService } from '@auth0/auth0-angular';
import { mergeMap, tap } from 'rxjs/operators';
import { Browser } from '@capacitor/browser';
import { App } from '@capacitor/app';

const callbackUri = `dev.peakcode.tablegenius://table-genius.eu.auth0.com/capacitor/dev.peakcode.tablegenius/callback`;

@Component({
  selector: 'app-root',
  templateUrl: 'app.component.html',
  styleUrls: ['app.component.scss'],
})
export class AppComponent {
  // Import the AuthService module from the Auth0 Angular SDK
  constructor(
    public auth: AuthService,
    private readonly ngZone: NgZone
  ) {}

  ngOnInit(): void {
    // Use Capacitor's App plugin to subscribe to the `appUrlOpen` event
    this.auth.error$.pipe(tap(x => console.error(x))).subscribe();
    App.addListener('appUrlOpen', ({ url }) => {
      // Must run inside an NgZone for Angular to pick up the changes
      // https://capacitorjs.com/docs/guides/angular
      this.ngZone.run(() => {
        if (url?.startsWith(callbackUri)) {
          // If the URL is an authentication callback URL..
          if (
            url.includes('state=') &&
            (url.includes('error=') || url.includes('code='))
          ) {
            // Call handleRedirectCallback and close the browser
            this.auth
              .handleRedirectCallback(url)
              .pipe(mergeMap(() => Browser.close()))
              .subscribe();
          } else {
            Browser.close().then(r => console.error(''));
          }
        }
      });
    });
  }
}
