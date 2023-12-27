import { Injectable } from '@angular/core';
import { Dispatch } from '@ngxs-labs/dispatch-decorator';
import { Select } from '@ngxs/store';
import { Observable } from 'rxjs';
import { TableAssignmentState } from './table-assignment.state';
import {
  AddReservationAssignment,
  LoadReservations,
  LoadTables,
  SetSelectedArea,
  SetSelectedAreaSlot,
  SetSelectedDate,
} from './table-assignment.actions';
import { AppState } from '../../core/app-state';
import { IReservation } from '../../domain/reservation';
import { ITableWithStatus } from '../../domain/table';
import { IReservationAssignmentModel } from '../../models/reservation-assignment-model';

@Injectable()
export class TableAssignmentFacade {
  @Select(TableAssignmentState.reservations)
  reservations: Observable<IReservation[]>;
  @Select(TableAssignmentState.tables)
  tables: Observable<ITableWithStatus[]>;
  @Select(TableAssignmentState.selectedDate)
  selectedDate: Observable<Date>;
  @Select(TableAssignmentState.selectedArea)
  selectedArea: Observable<string>;
  @Select(TableAssignmentState.selectedAreaSlot)
  selectedAreaSlot: Observable<string>;

  constructor(private readonly appState: AppState) {}

  @Dispatch()
  loadReservations = () => new LoadReservations();
  @Dispatch()
  loadTables = (areaSlotId: string) => new LoadTables(areaSlotId);
  loadReservations$ = () => this.appState.dispatch(new LoadReservations());
  @Dispatch()
  setSelectedDate = (date: Date) => new SetSelectedDate(date);
  @Dispatch()
  setSelectedArea = (areaId: string) => new SetSelectedArea(areaId);
  @Dispatch()
  setSelectedAreaSlot = (areaSlotId: string) =>
    new SetSelectedAreaSlot(areaSlotId);
  @Dispatch()
  addReservationAssignment = (model: IReservationAssignmentModel) =>
    new AddReservationAssignment(model);
}
