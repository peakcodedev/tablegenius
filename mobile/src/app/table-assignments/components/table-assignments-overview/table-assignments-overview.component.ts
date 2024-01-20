import { Component, OnDestroy, OnInit } from '@angular/core';
import {
  combineLatest,
  filter,
  map,
  Observable,
  startWith,
  Subject,
  take,
  takeUntil,
  tap,
} from 'rxjs';
import { AreasHelper } from '../../../areas-core/helpers/areas.helper';
import { TableAssignmentFacade } from '../../state/table-assignment.facade';
import { IReservation } from '../../../domain/reservation';
import { ITableWithStatus } from '../../../domain/table';
import { AreaFacade } from '../../../areas-core/state/area.facade';
import { AreaSlotsHelper } from '../../../area-slots-core/helpers/area-slots.helper';
import { IReservationAssignmentModel } from '../../../models/reservation-assignment-model';
import { ITableReservationAssignmentModel } from '../../../models/table-reservation-assignment-model';
import { ModalController, Platform } from '@ionic/angular';
import { ReservationSelectionModalComponent } from '../../modals/reservation-selection/reservation-selection-modal.component';
import { TableSelectionModalComponent } from '../../modals/table-selection/table-selection-modal.component';

@Component({
  selector: 'table-assignments-overview',
  templateUrl: './table-assignments-overview.component.html',
  styleUrls: ['./table-assignments-overview.component.scss'],
})
export class TableAssignmentsOverviewComponent implements OnInit, OnDestroy {
  minDate = new Date().toISOString();
  areas: Observable<any[]>;
  areaSlots: any[];
  selectedArea = '';
  selectedAreaSlot = '';
  selectedDate: string = null;
  reservations$: Observable<IReservation[]>;
  tables$: Observable<ITableWithStatus[]>;
  selectedTables: ITableWithStatus[] = [];
  selectedReservation: IReservation;
  destroy = new Subject<void>();
  isTabletView: boolean = false;

  constructor(
    private readonly areasHelper: AreasHelper,
    readonly facade: TableAssignmentFacade,
    readonly areaFacade: AreaFacade,
    private readonly areaSlotsHelper: AreaSlotsHelper,
    private readonly platform: Platform,
    private readonly modalController: ModalController
  ) {}

  ngOnInit(): void {
    this.isTabletView = this.platform.is('tablet');
    this.facade.loadReservations();
    this.facade.setSelectedDate(new Date());
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
      .pipe(
        tap(value => {
          this.selectedDate = value?.toISOString();
        })
      )
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

  onDateSelect(event: CustomEvent): void {
    this.facade.setSelectedDate(new Date(event.detail.value));
    this.onDiscard();
  }

  onAreaSelect(event: CustomEvent): void {
    this.facade.setSelectedArea(event.detail.value);
    this.facade.setSelectedAreaSlot(undefined);
    this.onDiscard();
  }

  onAreaSlotSelect(event: CustomEvent): void {
    this.facade.setSelectedAreaSlot(event.detail.value);
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

  async openReservationSelectionModal() {
    this.reservations$
      .pipe(
        filter(list => Boolean(list) && list.length > 0),
        takeUntil(this.destroy),
        take(1)
      )
      .subscribe(async reservations => {
        const modal = await this.modalController.create({
          component: ReservationSelectionModalComponent,
          componentProps: {
            reservations: reservations,
          },
        });
        await modal.present();
        const { data, role } = await modal.onWillDismiss();
        if (role === 'confirm') {
          this.selectedReservation = data;
        }
      });
  }

  async openTableSelectionModal() {
    this.tables$
      .pipe(
        filter(list => Boolean(list) && list.length > 0),
        takeUntil(this.destroy),
        take(1)
      )
      .subscribe(async tables => {
        const modal = await this.modalController.create({
          component: TableSelectionModalComponent,
          componentProps: {
            tables: tables,
            selectedTables: this.selectedTables,
          },
        });
        await modal.present();
        const { data, role } = await modal.onWillDismiss();
        if (role === 'confirm') {
          this.selectedTables.push(data);
        }
      });
  }
}
