import {
  APP_INITIALIZER,
  ErrorHandler,
  LOCALE_ID,
  NgModule,
} from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { Router, RouteReuseStrategy } from '@angular/router';

import { IonicModule, IonicRouteStrategy } from '@ionic/angular';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AuthHttpInterceptor, AuthModule } from '@auth0/auth0-angular';
import config from '../../capacitor.config';
import { MainModule } from './main/main.module';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { NgxsModule } from '@ngxs/store';
import { environment } from '../environments/environment';
import { NgxsLoggerPluginModule } from '@ngxs/logger-plugin';
import { NgxsFormPluginModule } from '@ngxs/form-plugin';
import { NgxsRouterPluginModule } from '@ngxs/router-plugin';
import { NgxsStoragePluginModule } from '@ngxs/storage-plugin';
import { CoreState } from './core/state/core.state';
import { LocationSelectionState } from './location-selector/state/location-selection.state';
import { CoreModule } from './core/core.module';
import { TenantInterceptor } from './core/intercepters/tenant-intercepter';
import * as Sentry from '@sentry/angular-ivy';
import { registerLocaleData } from '@angular/common';
import locale from '@angular/common/locales/de-CH';
const redirect_uri = `${config.appId}://table-genius.eu.auth0.com/capacitor/${config.appId}/callback`;
registerLocaleData(locale);

@NgModule({
  declarations: [AppComponent],
  imports: [
    BrowserModule,
    IonicModule.forRoot({}),
    AppRoutingModule,
    MainModule,
    HttpClientModule,
    NgxsModule.forRoot([], {
      developmentMode: !environment.production,
    }),
    NgxsLoggerPluginModule.forRoot({
      disabled: true,
    }),
    NgxsFormPluginModule.forRoot(),
    NgxsRouterPluginModule.forRoot(),
    NgxsStoragePluginModule.forRoot({
      key: [CoreState, LocationSelectionState],
    }),
    CoreModule,
    AuthModule.forRoot({
      domain: 'table-genius.eu.auth0.com',
      clientId: 'YAFNuJZe0fbpO4o5YoEIJHNkQbaswU7l',
      useRefreshTokens: true,
      useRefreshTokensFallback: false,
      authorizationParams: {
        redirect_uri: redirect_uri,
        audience: 'https://tablegenius-api.peakcode.dev',
      },
      cacheLocation: 'localstorage',
      issuer: 'table-genius.eu.auth0.com',
      httpInterceptor: {
        allowedList: [
          'http://localhost:8181/*',
          'https://tablegenius-api.peakcode.dev/*',
          'https://tablegenius-api-testing.peakcode.dev/*',
        ],
      },
    }),
    AuthModule,
  ],
  providers: [
    { provide: RouteReuseStrategy, useClass: IonicRouteStrategy },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthHttpInterceptor,
      multi: true,
    },
    {
      provide: ErrorHandler,
      useValue: Sentry.createErrorHandler({
        showDialog: false,
      }),
    },
    {
      provide: Sentry.TraceService,
      deps: [Router],
    },
    {
      provide: APP_INITIALIZER,
      useFactory: () => () => {},
      deps: [Sentry.TraceService],
      multi: true,
    },
    { provide: LOCALE_ID, useValue: 'de-CH' },
    { provide: HTTP_INTERCEPTORS, useClass: TenantInterceptor, multi: true },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
