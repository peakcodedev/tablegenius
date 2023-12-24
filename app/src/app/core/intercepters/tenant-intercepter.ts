import { Injectable } from '@angular/core';
import {
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest,
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { AppState } from '../app-state';
import { CoreState } from '../state/core.state';
import { environment } from '../../../environments/environment';

@Injectable()
export class TenantInterceptor implements HttpInterceptor {
  constructor(private readonly appState: AppState) {}
  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    const tenantId = this.appState.select(CoreState.tenantId);
    if (req.url.includes(environment.apiUrl) && tenantId) {
      const modifiedReq = req.clone({
        headers: req.headers.set('X-TenantId', tenantId),
      });
      return next.handle(modifiedReq);
    }
    return next.handle(req);
  }
}
