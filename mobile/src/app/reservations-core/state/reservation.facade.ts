import { Injectable } from '@angular/core';
import { Dispatch } from '@ngxs-labs/dispatch-decorator';
import { Select } from '@ngxs/store';
import { map, Observable } from 'rxjs';
import { ReservationState } from './reservation.state';
import {
  AddReservation,
  DeleteReservation,
  LoadReservations,
  SetSearchString,
  SetSelectedDate,
  UpdateReservation,
} from './reservation.actions';
import { AppState } from '../../core/app-state';
import { IReservation } from '../../domain/reservation';

@Injectable()
export class ReservationFacade {
  @Select(ReservationState.reservations)
  reservations: Observable<IReservation[]>;
  @Select(ReservationState.loading)
  loading: Observable<boolean>;
  @Select(ReservationState.errorMessage)
  errorMessage: Observable<string>;
  @Select(ReservationState.searchString)
  searchString: Observable<string>;
  @Select(ReservationState.selectedDate)
  selectedDate: Observable<Date>;

  constructor(private readonly appState: AppState) {}

  byId$(id: string) {
    return this.appState
      .select$(ReservationState.reservation)
      .pipe(map(selector => selector(id)));
  }

  @Dispatch()
  loadReservations = () => new LoadReservations();
  loadReservations$ = () => this.appState.dispatch(new LoadReservations());

  @Dispatch()
  addReservation = () => new AddReservation();

  @Dispatch()
  deleteReservation = (reservationId: string) =>
    new DeleteReservation(reservationId);

  @Dispatch()
  updateReservation = (reservationId: string) =>
    new UpdateReservation(reservationId);

  @Dispatch()
  setSearchString = (searchString: string) => new SetSearchString(searchString);
  @Dispatch()
  setSelectedDate = (date: Date) => new SetSelectedDate(date);
}
