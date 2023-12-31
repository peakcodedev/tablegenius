import { Injectable } from '@angular/core';
import { Action, Selector, State, StateContext } from '@ngxs/store';
import {
  AddReservationAssignment,
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
import { TableService } from '../../tables-core/services/table.service';
import { ReservationStateModel } from '../../reservations-core/state/reservation-state-model';
import { ReservationAssignmentService } from '../services/reservation-assignment.service';
import { ToastrService } from 'ngx-toastr';
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
    private readonly tableService: TableService,
    private readonly reservationAssignmentService: ReservationAssignmentService,
    private readonly toastrService: ToastrService,
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
      dateTime: getState().selectedDate,
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
          this.toastrService.success(
            'Die Zuweisung wurde erfolgreich hinzugefügt.'
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
