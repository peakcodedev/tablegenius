import { Component, OnDestroy, OnInit } from '@angular/core';
import {
  combineLatest,
  filter,
  map,
  Observable,
  startWith,
  Subject,
  takeUntil,
  tap,
} from 'rxjs';
import { AreasHelper } from '../../../areas-core/helpers/areas.helper';
import { TableAssignmentFacade } from '../../state/table-assignment.facade';
import { DropdownChangeEvent } from 'primeng/dropdown';
import { IReservation } from '../../../domain/reservation';
import { ITableWithStatus } from '../../../domain/table';
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
  areas: Observable<any[]>;
  areaSlots: any[];
  selectedArea = '';
  selectedAreaSlot = '';
  selectedDate: Date = null;
  reservations$: Observable<IReservation[]>;
  tables$: Observable<ITableWithStatus[]>;
  selectedTables: ITableWithStatus[] = [];
  selectedReservation: IReservation;
  draggedTable: ITableWithStatus;
  draggedReservation: IReservation;
  destroy = new Subject<void>();

  constructor(
    private readonly areasHelper: AreasHelper,
    readonly facade: TableAssignmentFacade,
    readonly areaFacade: AreaFacade,
    private readonly areaSlotsHelper: AreaSlotsHelper
  ) {}

  ngOnInit(): void {
    this.facade.loadReservations();
    this.areas = this.areasHelper.availableAreas();
    this.areaSlots = this.areaSlotsHelper.availableAreaSlots(undefined);
    this.facade.selectedArea
      .pipe(
        tap(value => {
          this.selectedArea = value;
          this.areaSlots = this.areaSlotsHelper.availableAreaSlots(value);
        })
      )
      .subscribe();
    this.facade.selectedDate
      .pipe(tap(value => (this.selectedDate = value)))
      .subscribe();
    this.facade.selectedAreaSlot
      .pipe(
        tap((value: string) => {
          this.selectedAreaSlot = value;
          this.loadData();
        })
      )
      .subscribe();
    this.reservations$ = combineLatest([
      this.facade.selectedDate.pipe(startWith(undefined)),
      this.facade.reservations.pipe(startWith([])),
    ]).pipe(
      takeUntil(this.destroy),
      filter(([selectedDate, _reservations]) => Boolean(selectedDate)),
      map(([selectedDate, reservations]) =>
        reservations.filter(
          reservation =>
            new Date(reservation.bookingDate)?.setHours(0, 0, 0, 0) ===
            selectedDate?.setHours(0, 0, 0, 0)
        )
      )
    );
    this.tables$ = combineLatest([
      this.facade.selectedArea.pipe(startWith(undefined)),
      this.facade.tables.pipe(startWith([])),
    ]).pipe(
      takeUntil(this.destroy),
      filter(([selectedArea, _tables]) => Boolean(selectedArea)),
      map(([selectedArea, tables]) =>
        tables.filter(table => table.areaId === selectedArea)
      ),
      map(tables => tables.sort((a, b) => a.tableNumber - b.tableNumber))
    );
  }

  onDateSelect(date: Date): void {
    this.facade.setSelectedDate(date);
    this.onDiscard();
    this.loadData();
  }

  onAreaSelect(event: DropdownChangeEvent): void {
    this.facade.setSelectedArea(event.value);
    this.facade.setSelectedAreaSlot(undefined);
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
      bookingDate: new Date(this.selectedReservation.bookingDate),
      tableReservationAssignments: assignments,
    };
    this.facade.addReservationAssignment(model);
    this.onDiscard();
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
        console.error('No type supplied');
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
        console.error('No type supplied');
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
        console.error('No type supplied');
    }
  }

  loadData(): void {
    if (!this.selectedArea || !this.selectedAreaSlot || !this.selectedDate) {
      return;
    }
    this.facade.loadTables(this.selectedAreaSlot);
  }

  ngOnDestroy(): void {
    this.facade.clearState();
    this.destroy.next();
    this.destroy.complete();
  }

  tableIsSelected(table: ITableWithStatus): boolean {
    if (table.taken) return true;
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
