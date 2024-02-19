import { Injectable } from '@angular/core';
import { Action, Selector, State, StateContext } from '@ngxs/store';
import {
  ClearState,
  DeleteReservationAssignment,
  LoadAssignments,
  LoadFreeTables,
  ResetErrorMessage,
  SetSelectedArea,
  SetSelectedAreaSlot,
  SetSelectedDate,
} from './reservation-assignment.actions';
import { ReservationAssignmentsOverviewService } from '../services/reservation-assignments-overview.service';
import { catchError, finalize, of, tap } from 'rxjs';
import { ReservationAssignmentStateModel } from './reservation-assignment-state-model';
import { IDateFilterModel } from '../../models/date-filter-model';
import { ReservationAssignmentsService } from '../services/reservation-assignments.service';

const defaults: ReservationAssignmentStateModel = {
  assignments: [],
  selectedArea: '',
  selectedAreaSlot: '',
  selectedDate: undefined,
  freeTables: [],
  loading: false,
};

@State<ReservationAssignmentStateModel>({
  name: 'reservationAssignments',
  defaults,
})
@Injectable()
export class ReservationAssignmentState {
  @Selector() static selectedDate(state: ReservationAssignmentStateModel) {
    return state.selectedDate;
  }

  @Selector() static loading(state: ReservationAssignmentStateModel) {
    return state.loading;
  }

  @Selector() static selectedArea(state: ReservationAssignmentStateModel) {
    return state.selectedArea;
  }

  @Selector() static selectedAreaSlot(state: ReservationAssignmentStateModel) {
    return state.selectedAreaSlot;
  }

  @Selector() static assignments(state: ReservationAssignmentStateModel) {
    return state.assignments;
  }

  @Selector() static freeTables(state: ReservationAssignmentStateModel) {
    return state.freeTables;
  }

  @Selector() static assignment(state: ReservationAssignmentStateModel) {
    return (id: string) => state.assignments.find(e => e.id === id);
  }

  constructor(
    private readonly reservationAssignmentsOverviewService: ReservationAssignmentsOverviewService,
    private readonly reservationAssignmentsService: ReservationAssignmentsService
  ) {}

  @Action(SetSelectedDate)
  setSelectedDate(
    { patchState }: StateContext<ReservationAssignmentStateModel>,
    action: SetSelectedDate
  ) {
    patchState({
      selectedDate: action.selectedDate,
    });
  }

  @Action(SetSelectedArea)
  setSelectedArea(
    { patchState }: StateContext<ReservationAssignmentStateModel>,
    action: SetSelectedArea
  ) {
    patchState({
      selectedArea: action.areaId,
    });
  }

  @Action(SetSelectedAreaSlot)
  setSelectedAreaSlot(
    { patchState }: StateContext<ReservationAssignmentStateModel>,
    action: SetSelectedAreaSlot
  ) {
    patchState({
      selectedAreaSlot: action.areaSlotId,
    });
  }

  @Action(ClearState)
  clear(
    { patchState }: StateContext<ReservationAssignmentStateModel>,
    _action: SetSelectedAreaSlot
  ) {
    patchState(defaults);
  }

  @Action(LoadAssignments)
  loadAssignments(
    {
      patchState,
      dispatch,
      getState,
    }: StateContext<ReservationAssignmentStateModel>,
    {}: LoadAssignments
  ) {
    const model: IDateFilterModel = {
      dateTime: getState().selectedDate.toString().slice(0, 24),
    };
    return this.reservationAssignmentsOverviewService
      .getReservations(getState().selectedAreaSlot, model)
      .pipe(
        tap(response => {
          patchState({ assignments: response.data });
          dispatch(new ResetErrorMessage());
        }),
        catchError(error => {
          return of([]);
        })
      );
  }

  @Action(LoadFreeTables)
  loadFreeTables(
    {
      patchState,
      dispatch,
      getState,
    }: StateContext<ReservationAssignmentStateModel>,
    {}: LoadAssignments
  ) {
    const model: IDateFilterModel = {
      dateTime: getState().selectedDate.toString().slice(0, 24),
    };
    return this.reservationAssignmentsOverviewService
      .getFreeTables(getState().selectedAreaSlot, model)
      .pipe(
        tap(response => {
          patchState({ freeTables: response.data });
          dispatch(new ResetErrorMessage());
        }),
        catchError(error => {
          return of([]);
        })
      );
  }

  @Action(DeleteReservationAssignment)
  deleteReservationAssignment(
    context: StateContext<ReservationAssignmentStateModel>,
    action: DeleteReservationAssignment
  ) {
    context.patchState({ loading: true });
    return this.reservationAssignmentsService
      .deleteReservationAssignment(action.id)
      .pipe(
        tap(_ => {
          context.dispatch([new LoadFreeTables(), new LoadAssignments()]);
        }),
        catchError(error => {
          context.patchState({ loading: false });
          return of([]);
        }),
        finalize(() => {
          context.patchState({ loading: false });
        })
      );
  }
}
