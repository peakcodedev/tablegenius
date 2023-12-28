import { Injectable } from '@angular/core';
import { Dispatch } from '@ngxs-labs/dispatch-decorator';
import { Select } from '@ngxs/store';
import { map, Observable } from 'rxjs';
import { ReservationAssignmentState } from './reservation-assignment.state';
import {
  LoadAssignments,
  SetSelectedArea,
  SetSelectedAreaSlot,
  SetSelectedDate,
} from './reservation-assignment.actions';
import { AppState } from '../../core/app-state';
import { IReservation } from '../../domain/reservation';

@Injectable()
export class ReservationAssignmentFacade {
  @Select(ReservationAssignmentState.assignments)
  assignments: Observable<IReservation[]>;
  //@Select(ReservationAssignmentState.loading)
  //loading: Observable<boolean>;
  //@Select(ReservationAssignmentState.errorMessage)
  //errorMessage: Observable<string>;
  @Select(ReservationAssignmentState.selectedDate)
  selectedDate: Observable<Date>;
  @Select(ReservationAssignmentState.selectedArea)
  selectedArea: Observable<string>;
  @Select(ReservationAssignmentState.selectedAreaSlot)
  selectedAreaSlot: Observable<string>;

  constructor(private readonly appState: AppState) {}

  byId$(id: string) {
    return this.appState
      .select$(ReservationAssignmentState.assignment)
      .pipe(map(selector => selector(id)));
  }

  @Dispatch()
  loadAssignments = () => new LoadAssignments();
  loadAssignments$ = () => this.appState.dispatch(new LoadAssignments());
  @Dispatch()
  setSelectedDate = (date: Date) => new SetSelectedDate(date);
  @Dispatch()
  setSelectedArea = (areaId: string) => new SetSelectedArea(areaId);
  @Dispatch()
  setSelectedAreaSlot = (areaSlotId: string) =>
    new SetSelectedAreaSlot(areaSlotId);
}
