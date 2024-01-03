import { Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
  UrlTree,
} from '@angular/router';
import { Observable } from 'rxjs';
import { LocationSelectionFacade } from '../state/location-selection.facade';

@Injectable({
  providedIn: 'root',
})
export class CanActivateLocationSelection {
  constructor(
    private readonly locationSelectionFacade: LocationSelectionFacade
  ) {}
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ):
    | Observable<boolean | UrlTree>
    | Promise<boolean | UrlTree>
    | boolean
    | UrlTree {
    return this.locationSelectionFacade.loadLocationsOfUser$();
  }
}
