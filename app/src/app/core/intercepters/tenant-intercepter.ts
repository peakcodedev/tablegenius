import { Injectable } from '@angular/core';
import {
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest,
} from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class TenantInterceptor implements HttpInterceptor {
  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    const modifiedReq = req.clone({
      headers: req.headers.set(
        'X-TenantId',
        `722e574c-8295-4f71-89d5-87ac75bd5bd0`
      ),
    });
    return next.handle(modifiedReq);
  }
}
