import { Injectable } from '@angular/core';
import { Action, Selector, State, StateContext } from '@ngxs/store';
import {
  AddReservation,
  DeleteReservation,
  LoadReservations,
  ResetErrorMessage,
  SetSearchString,
  SetSelectedDate,
  UpdateReservation,
} from './reservation.actions';
import { ReservationService } from '../services/reservation.service';
import { catchError, finalize, of, tap } from 'rxjs';
import { ReservationStateModel } from './reservation-state-model';
import { ResetForm } from '@ngxs/form-plugin';
import {
  insertItem,
  patch,
  removeItem,
  updateItem,
} from '@ngxs/store/operators';
import { Navigate } from '@ngxs/router-plugin';
import { IReservation } from '../../domain/reservation';
//import { ToastrService } from 'ngx-toastr';

const defaults: ReservationStateModel = {
  data: [],
  errorMessage: '',
  selectedDate: null as any,
  loading: false,
  searchString: undefined,
  editReservationForm: {
    model: undefined,
    dirty: false,
    status: '',
    errors: {},
  },
  addReservationForm: {
    model: undefined,
    dirty: false,
    status: '',
    errors: {},
  },
};

@State<ReservationStateModel>({
  name: 'reservations',
  defaults,
})
@Injectable()
export class ReservationState {
  @Selector() static errorMessage(state: ReservationStateModel) {
    return state.errorMessage;
  }

  @Selector() static searchString(state: ReservationStateModel) {
    return state.searchString;
  }

  @Selector() static selectedDate(state: ReservationStateModel) {
    return state.selectedDate;
  }

  @Selector() static loading(state: ReservationStateModel) {
    return state.loading;
  }

  @Selector() static reservations(state: ReservationStateModel) {
    return state.data;
  }

  @Selector() static reservation(state: ReservationStateModel) {
    return (id: string) => state.data.find(e => e.id === id);
  }

  constructor(private readonly reservationService: ReservationService) {}

  @Action(ResetErrorMessage)
  resetErrorMessage(
    { patchState }: StateContext<ReservationStateModel>,
    {}: ResetErrorMessage
  ) {
    patchState({
      errorMessage: '',
    });
  }

  @Action(SetSelectedDate)
  setSelectedDate(
    { patchState }: StateContext<ReservationStateModel>,
    action: SetSelectedDate
  ) {
    patchState({
      selectedDate: action.selectedDate,
    });
  }

  @Action(SetSearchString)
  setSearchString(
    { patchState }: StateContext<ReservationStateModel>,
    action: SetSearchString
  ) {
    patchState({
      searchString: action.searchString,
    });
  }

  @Action(LoadReservations)
  loadReservations(
    { patchState, dispatch }: StateContext<ReservationStateModel>,
    {}: LoadReservations
  ) {
    patchState({ loading: true });
    return this.reservationService.getReservations().pipe(
      tap(response => {
        patchState({ data: response.data, loading: false });
        dispatch(new ResetErrorMessage());
      }),
      catchError(error => {
        patchState({ errorMessage: error, loading: false });
        return of([]);
      })
    );
  }

  @Action(AddReservation)
  addReservation(
    context: StateContext<ReservationStateModel>,
    {}: AddReservation
  ) {
    context.patchState({ loading: true });
    let formValues = context.getState().addReservationForm.model;
    return this.reservationService.createReservation(formValues).pipe(
      tap(res => {
        context.setState(
          patch<ReservationStateModel>({
            data: insertItem<IReservation>(res.data),
          })
        );
        context.dispatch([
          new ResetForm({ path: 'reservations.addReservationForm' }),
          new Navigate(['/tabs/reservations']),
        ]);
      }),
      catchError(error => {
        context.patchState({ errorMessage: error, loading: false });
        return of([]);
      }),
      finalize(() => {
        context.patchState({ loading: false });
      })
    );
  }

  @Action(UpdateReservation)
  updateReservation(
    context: StateContext<ReservationStateModel>,
    action: UpdateReservation
  ) {
    context.patchState({ loading: true });
    let formValues = context.getState().editReservationForm.model;
    return this.reservationService
      .updateReservation(action.reservationId, formValues)
      .pipe(
        tap(res => {
          context.setState(
            patch<ReservationStateModel>({
              data: updateItem<IReservation>(
                reservation => reservation.id === action.reservationId,
                res.data
              ),
            })
          );
          context.dispatch([
            new ResetForm({ path: 'reservations.editReservationForm' }),
            new Navigate(['/tabs/reservations']),
          ]);
        }),
        catchError(error => {
          context.patchState({ errorMessage: error, loading: false });
          return of([]);
        }),
        finalize(() => {
          context.patchState({ loading: false });
        })
      );
  }

  @Action(DeleteReservation)
  deleteReservation(
    context: StateContext<ReservationStateModel>,
    action: DeleteReservation
  ) {
    context.patchState({ loading: true });
    return this.reservationService.deleteReservation(action.reservationId).pipe(
      tap(_ => {
        context.setState(
          patch<ReservationStateModel>({
            data: removeItem<IReservation>(
              location => location.id === action.reservationId
            ),
          })
        );
      }),
      catchError(error => {
        context.patchState({ errorMessage: error, loading: false });
        return of([]);
      }),
      finalize(() => {
        context.patchState({ loading: false });
      })
    );
  }
}
