import {
  APP_INITIALIZER,
  ErrorHandler,
  LOCALE_ID,
  NgModule,
} from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NgxsModule } from '@ngxs/store';
import { NgxsLoggerPluginModule } from '@ngxs/logger-plugin';
import { NgxsReduxDevtoolsPluginModule } from '@ngxs/devtools-plugin';
import { NgxsFormPluginModule } from '@ngxs/form-plugin';
import { NgxsRouterPluginModule } from '@ngxs/router-plugin';
import { environment } from '../environments/environment';
import * as Sentry from '@sentry/angular-ivy';
import { Router } from '@angular/router';
import { registerLocaleData } from '@angular/common';
import locale from '@angular/common/locales/de-CH';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { DesignModule } from './design/design.module';
import { NavigationModule } from './navigation/navigation.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CoreModule } from './core/core.module';
import { ToastrModule } from 'ngx-toastr';
import { NgxsStoragePluginModule } from '@ngxs/storage-plugin';
import { CoreState } from './core/state/core.state';
import { LocationSelectionState } from './location-selector/state/location-selection.state';
import { AuthHttpInterceptor, AuthModule } from '@auth0/auth0-angular';
import { MainModule } from './main/main/main.module';
import { TenantInterceptor } from './core/intercepters/tenant-intercepter';

registerLocaleData(locale);

@NgModule({
  declarations: [AppComponent],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot({
      preventDuplicates: true,
      enableHtml: true,
      progressBar: true,
      tapToDismiss: true,
      newestOnTop: true,
      timeOut: 5000,
      positionClass: 'toast-bottom-left',
    }),
    HttpClientModule,
    NgxsModule.forRoot([], {
      developmentMode: !environment.production,
    }),
    NgxsLoggerPluginModule.forRoot(),
    NgxsReduxDevtoolsPluginModule.forRoot(),
    NgxsFormPluginModule.forRoot(),
    NgxsRouterPluginModule.forRoot(),
    NgxsStoragePluginModule.forRoot({
      key: [CoreState, LocationSelectionState],
    }),
    DesignModule,
    NavigationModule,
    CoreModule,
    MainModule,
    AuthModule.forRoot({
      domain: 'table-genius.eu.auth0.com',
      clientId: 'cAPz35Fqzu9VZV4W2Z6tF5AdHGCveT3x',
      authorizationParams: {
        redirect_uri: window.location.origin,
        audience: 'https://tablegenius-api.peakcode.dev',
      },
      useRefreshTokens: true,
      useRefreshTokensFallback: true,
      cacheLocation: 'localstorage',
      issuer: 'table-genius.eu.auth0.com',
      httpInterceptor: {
        allowedList: ['http://localhost:8181/*'],
      },
    }),
  ],
  providers: [
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
