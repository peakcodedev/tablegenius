import { Component, OnDestroy, OnInit } from '@angular/core';
import { combineLatestWith, filter, map, Observable, tap } from 'rxjs';
import { AreasHelper } from '../../../areas-core/helpers/areas.helper';
import { TableAssignmentFacade } from '../../state/table-assignment.facade';
import { DropdownChangeEvent } from 'primeng/dropdown';
import { IReservation } from '../../../domain/reservation';
import { ITable } from '../../../domain/table';
import { AreaFacade } from '../../../areas-core/state/area.facade';
import { AreaSlotsHelper } from '../../../area-slots-core/helpers/area-slots.helper';
import { IReservationAssignmentModel } from '../../../models/reservation-assignment-model';
import { ITableReservationAssignmentModel } from '../../../models/table-reservation-assignment-model';

@Component({
  selector: 'table-assignments-overview',
  templateUrl: './table-assignments-overview.component.html',
  styleUrls: ['./table-assignments-overview.component.scss'],
})
export class TableAssignmentsOverviewComponent implements OnInit, OnDestroy {
  minDate = new Date();
  areas: Observable<any[]>;
  areaSlots: any[];
  selectedArea = '';
  selectedAreaSlot = '';
  selectedDate: Date = null;
  reservations$: Observable<IReservation[]>;
  tables$: Observable<ITable[]>;
  selectedTables: ITable[] = [];
  selectedReservation: IReservation;
  draggedTable: ITable;
  draggedReservation: IReservation;

  constructor(
    private readonly areasHelper: AreasHelper,
    readonly facade: TableAssignmentFacade,
    readonly areaFacade: AreaFacade,
    private readonly areaSlotsHelper: AreaSlotsHelper
  ) {}

  ngOnInit(): void {
    this.facade.loadReservations();
    this.facade.loadTables();
    this.areas = this.areasHelper.availableAreas();
    this.areaSlots = this.areaSlotsHelper.availableAreaSlots(undefined);
    this.facade.selectedArea
      .pipe(
        tap(value => {
          this.selectedArea = value;
          console.error(this.areaSlotsHelper.availableAreaSlots(value));
          this.areaSlots = this.areaSlotsHelper.availableAreaSlots(value);
        })
      )
      .subscribe();
    this.facade.selectedDate
      .pipe(tap(value => (this.selectedDate = value)))
      .subscribe();
    this.facade.selectedAreaSlot
      .pipe(tap(value => (this.selectedAreaSlot = value)))
      .subscribe();
    this.reservations$ = this.facade.selectedDate.pipe(
      combineLatestWith(this.facade.reservations),
      filter(([selectedDate, _reservations]) => Boolean(selectedDate)),
      map(([selectedDate, reservations]) =>
        reservations.filter(
          reservation =>
            new Date(reservation.bookingDate).setHours(0, 0, 0, 0) ===
            selectedDate.setHours(0, 0, 0, 0)
        )
      )
    );
    this.tables$ = this.facade.selectedArea.pipe(
      combineLatestWith(this.facade.tables),
      filter(([selectedArea, _tables]) => Boolean(selectedArea)),
      map(([selectedArea, tables]) =>
        tables.filter(table => table.areaId === selectedArea)
      ),
      map(tables => tables.sort((a, b) => a.tableNumber - b.tableNumber)),
      tap(values => console.error(values))
    );
  }

  onDateSelect(date: Date): void {
    this.facade.setSelectedDate(date);
    this.onDiscard();
  }

  onAreaSelect(event: DropdownChangeEvent): void {
    this.facade.setSelectedArea(event.value);
    this.onDiscard();
  }

  onAreaSlotSelect(event: DropdownChangeEvent): void {
    this.facade.setSelectedAreaSlot(event.value);
    this.onDiscard();
  }

  onSave(): void {
    const assignments = this.selectedTables.map(
      x => <ITableReservationAssignmentModel>{ tableId: x.id }
    );
    const model: IReservationAssignmentModel = {
      reservationId: this.selectedReservation.id,
      areaSlotId: this.selectedAreaSlot,
      bookingDate: this.selectedReservation.bookingDate,
      tableReservationAssignments: assignments,
    };
    this.facade.addReservationAssignment(model);
  }

  onDiscard(): void {
    this.selectedTables = [];
    this.selectedReservation = null;
  }

  onDragStart(object: any, type: 'table' | 'reservation'): void {
    switch (type) {
      case 'reservation':
        this.draggedReservation = object;
        break;
      case 'table':
        this.draggedTable = object;
        break;
      default:
        console.error('error');
    }
  }

  onDragEnd(type: 'table' | 'reservation'): void {
    switch (type) {
      case 'reservation':
        this.draggedReservation = null;
        break;
      case 'table':
        this.draggedTable = null;
        break;
      default:
        console.error('error');
    }
  }

  onDrop(type: 'table' | 'reservation'): void {
    switch (type) {
      case 'reservation':
        this.selectedReservation = this.draggedReservation;
        break;
      case 'table':
        this.selectedTables.push(this.draggedTable);
        break;
      default:
        console.error('error');
    }
  }

  ngOnDestroy(): void {
    this.facade.setSelectedDate(undefined);
    this.facade.setSelectedArea(undefined);
  }

  tableIsSelected(table: ITable): boolean {
    return (
      this.selectedTables && this.selectedTables.some(st => st.id === table.id)
    );
  }

  isAssigmentValid(): boolean {
    if (this.selectedTables.length === 0) return false;
    const places = this.selectedTables
      .map(x => x.capacity)
      .reduce((a, b) => a + b);
    return places >= this.selectedReservation?.count;
  }
}
