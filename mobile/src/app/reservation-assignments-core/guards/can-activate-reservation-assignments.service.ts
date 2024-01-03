import { Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
  UrlTree,
} from '@angular/router';
import { Observable } from 'rxjs';
import { ReservationAssignmentFacade } from '../state/reservation-assignment.facade';

@Injectable({
  providedIn: 'root',
})
export class CanActivateReservationAssignments {
  constructor(
    private readonly reservationAssignmentFacade: ReservationAssignmentFacade
  ) {}
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ):
    | Observable<boolean | UrlTree>
    | Promise<boolean | UrlTree>
    | boolean
    | UrlTree {
    return this.reservationAssignmentFacade.loadAssignments$();
  }
}
