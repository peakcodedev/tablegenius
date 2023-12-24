import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
import * as Sentry from "@sentry/angular-ivy";
import { AppModule } from './app/app.module';
import { ExtraErrorData, HttpClient } from "@sentry/integrations";

Sentry.init({
  dsn: "https://1f42216301d3e366e2811c0dbfb3f30e@o4506435432611840.ingest.sentry.io/4506435448078336",
  enabled: false,
  environment: "env needs to be definied",
  integrations: [
    new Sentry.BrowserTracing({
      routingInstrumentation: Sentry.routingInstrumentation,
    }),
    new ExtraErrorData(),
    new HttpClient(),
  ],
  tracesSampleRate: 0.1,
});

platformBrowserDynamic().bootstrapModule(AppModule)
  .catch(err => console.error(err));
