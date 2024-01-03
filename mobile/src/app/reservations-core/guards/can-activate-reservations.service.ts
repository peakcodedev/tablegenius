import { Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
  UrlTree,
} from '@angular/router';
import { Observable } from 'rxjs';
import { ReservationFacade } from '../state/reservation.facade';

@Injectable({
  providedIn: 'root',
})
export class CanActivateReservations {
  constructor(private readonly reservationFacade: ReservationFacade) {}
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ):
    | Observable<boolean | UrlTree>
    | Promise<boolean | UrlTree>
    | boolean
    | UrlTree {
    return this.reservationFacade.loadReservations$();
  }
}
