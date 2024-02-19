import { Injectable } from '@angular/core';
import { Action, Selector, State, StateContext } from '@ngxs/store';
import {
  AddReservationAssignment,
  ClearState,
  LoadReservations,
  LoadTables,
  ResetErrorMessage,
  SetSelectedArea,
  SetSelectedAreaSlot,
  SetSelectedDate,
} from './table-assignment.actions';
import { catchError, finalize, of, tap } from 'rxjs';
import { TableAssignmentStateModel } from './table-assignment-state-model';
import { ReservationService } from '../../reservations-core/services/reservation.service';
import { ReservationStateModel } from '../../reservations-core/state/reservation-state-model';
import { ReservationAssignmentService } from '../services/reservation-assignment.service';
import { AreaSlotService } from '../../area-slots-core/services/area-slot.service';
import { IDateFilterModel } from '../../models/date-filter-model';

const defaults: TableAssignmentStateModel = {
  assignments: [],
  reservations: [],
  selectedArea: undefined,
  selectedAreaSlot: undefined,
  selectedDate: undefined,
  tables: [],
};

@State<TableAssignmentStateModel>({
  name: 'tableAssignments',
  defaults,
})
@Injectable()
export class TableAssignmentState {
  @Selector() static selectedDate(state: TableAssignmentStateModel) {
    return state.selectedDate;
  }

  @Selector() static selectedArea(state: TableAssignmentStateModel) {
    return state.selectedArea;
  }

  @Selector() static selectedAreaSlot(state: TableAssignmentStateModel) {
    return state.selectedAreaSlot;
  }

  @Selector() static reservations(state: TableAssignmentStateModel) {
    return state.reservations;
  }

  @Selector() static tables(state: TableAssignmentStateModel) {
    return state.tables;
  }

  constructor(
    private readonly reservationService: ReservationService,
    private readonly reservationAssignmentService: ReservationAssignmentService,
    private readonly areaSlotService: AreaSlotService
  ) {}

  @Action(SetSelectedDate)
  setSelectedDate(
    { patchState }: StateContext<TableAssignmentStateModel>,
    action: SetSelectedDate
  ) {
    patchState({
      selectedDate: action.selectedDate,
    });
  }

  @Action(SetSelectedArea)
  setSelectedArea(
    { patchState }: StateContext<TableAssignmentStateModel>,
    action: SetSelectedArea
  ) {
    patchState({
      selectedArea: action.areaId,
    });
  }

  @Action(SetSelectedAreaSlot)
  setSelectedAreaSlot(
    { patchState }: StateContext<TableAssignmentStateModel>,
    action: SetSelectedAreaSlot
  ) {
    patchState({
      selectedAreaSlot: action.areaSlotId,
    });
  }

  @Action(ClearState)
  clearState(
    { patchState }: StateContext<TableAssignmentStateModel>,
    _: SetSelectedAreaSlot
  ) {
    patchState(defaults);
  }

  @Action(LoadReservations)
  loadReservations(
    { patchState, dispatch }: StateContext<TableAssignmentStateModel>,
    {}: LoadReservations
  ) {
    return this.reservationService.getUnassignedReservations().pipe(
      tap(response => {
        patchState({ reservations: response.data });
        dispatch(new ResetErrorMessage());
      }),
      catchError(error => {
        return of([]);
      })
    );
  }

  @Action(LoadTables)
  loadTables(
    { patchState, dispatch, getState }: StateContext<TableAssignmentStateModel>,
    action: LoadTables
  ) {
    const model: IDateFilterModel = {
      dateTime: getState().selectedDate.toString().slice(0, 24),
    };
    return this.areaSlotService
      .getTablesWithStatus(action.areaSlotId, model)
      .pipe(
        tap(response => {
          patchState({ tables: response.data });
          dispatch(new ResetErrorMessage());
        }),
        catchError(error => {
          return of([]);
        })
      );
  }

  @Action(AddReservationAssignment)
  addReservationAssignment(
    context: StateContext<ReservationStateModel>,
    action: AddReservationAssignment
  ) {
    return this.reservationAssignmentService
      .createReservationAssignment(action.model)
      .pipe(
        tap(res => {
          context.dispatch(new LoadReservations());
          context.dispatch(new LoadTables(action.model.areaSlotId));
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
