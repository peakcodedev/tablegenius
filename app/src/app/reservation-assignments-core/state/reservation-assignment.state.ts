import { Injectable } from '@angular/core';
import { Action, Selector, State, StateContext } from '@ngxs/store';
import {
  LoadAssignments,
  ResetErrorMessage,
  SetSelectedArea,
  SetSelectedAreaSlot,
  SetSelectedDate,
} from './reservation-assignment.actions';
import { ReservationAssignmentsService } from '../services/reservation-assignments.service';
import { catchError, of, tap } from 'rxjs';
import { ReservationAssignmentStateModel } from './reservation-assignment-state-model';
import { TableAssignmentStateModel } from '../../table-assignments/state/table-assignment-state-model';

const defaults: ReservationAssignmentStateModel = {
  assignments: [],
  selectedArea: '',
  selectedAreaSlot: '',
  selectedDate: undefined,
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

  @Selector() static selectedArea(state: ReservationAssignmentStateModel) {
    return state.selectedArea;
  }

  @Selector() static selectedAreaSlot(state: ReservationAssignmentStateModel) {
    return state.selectedAreaSlot;
  }

  @Selector() static assignments(state: ReservationAssignmentStateModel) {
    return state.assignments;
  }

  @Selector() static assignment(state: ReservationAssignmentStateModel) {
    return (id: string) => state.assignments.find(e => e.id === id);
  }

  constructor(
    private readonly reservationAssignmentsService: ReservationAssignmentsService
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

  @Action(LoadAssignments)
  loadAssignments(
    { patchState, dispatch }: StateContext<TableAssignmentStateModel>,
    {}: LoadAssignments
  ) {
    return this.reservationAssignmentsService.getUnassignedReservations().pipe(
      tap(response => {
        patchState({ reservations: response.data });
        dispatch(new ResetErrorMessage());
      }),
      catchError(error => {
        return of([]);
      })
    );
  }
}
