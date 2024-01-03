import { Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
  UrlTree,
} from '@angular/router';
import { Observable } from 'rxjs';
import { LocationFacade } from '../state/location.facade';

@Injectable({
  providedIn: 'root',
})
export class CanActivateLocations {
  constructor(private readonly locationFacade: LocationFacade) {}
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ):
    | Observable<boolean | UrlTree>
    | Promise<boolean | UrlTree>
    | boolean
    | UrlTree {
    return this.locationFacade.loadLocations$();
  }
}
