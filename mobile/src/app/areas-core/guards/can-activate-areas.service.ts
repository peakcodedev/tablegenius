import { Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
  UrlTree,
} from '@angular/router';
import { Observable } from 'rxjs';
import { AreaFacade } from '../state/area.facade';

@Injectable({
  providedIn: 'root',
})
export class CanActivateAreas {
  constructor(private readonly areaFacade: AreaFacade) {}
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ):
    | Observable<boolean | UrlTree>
    | Promise<boolean | UrlTree>
    | boolean
    | UrlTree {
    return this.areaFacade.loadAreas$();
  }
}
