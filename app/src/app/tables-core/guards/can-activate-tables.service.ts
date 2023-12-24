import { Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
  UrlTree,
} from '@angular/router';
import { Observable } from 'rxjs';
import { TableFacade } from '../state/table.facade';

@Injectable({
  providedIn: 'root',
})
export class CanActivateTables {
  constructor(private readonly tableFacade: TableFacade) {}
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ):
    | Observable<boolean | UrlTree>
    | Promise<boolean | UrlTree>
    | boolean
    | UrlTree {
    return this.tableFacade.loadTables$();
  }
}
