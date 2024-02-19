import { Component, OnDestroy, OnInit } from '@angular/core';
import { interval, Observable, Subject, takeUntil, tap } from 'rxjs';
import { AreasHelper } from '../../../areas-core/helpers/areas.helper';
import { AreaFacade } from '../../../areas-core/state/area.facade';
import { AreaSlotsHelper } from '../../../area-slots-core/helpers/area-slots.helper';
import { ReservationAssignmentFacade } from '../../../reservation-assignments-core/state/reservation-assignment.facade';
import { IReservation } from '../../../domain/reservation';
import { ReservationAssignmentDetailComponent } from '../reservation-assignment-detail/reservation-assignment-detail.component';
import { PopoverController } from '@ionic/angular';
import { ITable } from '../../../domain/table';
import { TableDetailComponent } from '../table-detail/table-detail.component';

@Component({
  selector: 'reservation-assignments-overview',
  templateUrl: './reservation-assignments-overview.component.html',
  styleUrls: ['./reservation-assignments-overview.component.scss'],
})
export class ReservationAssignmentsOverviewComponent
  implements OnInit, OnDestroy
{
  areas: Observable<any[]>;
  areaSlots: any[];
  selectedArea = '';
  selectedAreaSlot = '';
  selectedDate: string = undefined;
  selectedView: 'overview' | 'tables' = 'overview';
  destroy = new Subject<void>();

  constructor(
    private readonly areasHelper: AreasHelper,
    readonly facade: ReservationAssignmentFacade,
    readonly areaFacade: AreaFacade,
    private readonly areaSlotsHelper: AreaSlotsHelper,
    private readonly popoverController: PopoverController
  ) {}

  ngOnInit(): void {
    this.areas = this.areasHelper.availableAreas();
    this.facade.setSelectedDate(new Date());
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
          if (value instanceof Date) {
            this.selectedDate = value?.toISOString();
          }
        })
      )
      .subscribe();
    this.facade.selectedAreaSlot
      .pipe(
        tap((value: string) => {
          this.selectedAreaSlot = value;
          if (value) {
            this.loadData();
          }
        })
      )
      .subscribe();
    interval(1000 * 60 * 5)
      .pipe(takeUntil(this.destroy))
      .subscribe(_ => {
        this.loadData();
      });
  }

  onDateSelect(event: CustomEvent): void {
    this.facade.setSelectedDate(event.detail.value);
    this.loadData();
  }

  onAreaSelect(event: CustomEvent): void {
    this.facade.setSelectedArea(event.detail.value);
    this.facade.setSelectedAreaSlot(undefined);
  }

  onAreaSlotSelect(event: CustomEvent): void {
    this.facade.setSelectedAreaSlot(event.detail.value);
  }

  loadData(): void {
    if (!this.selectedArea || !this.selectedAreaSlot || !this.selectedDate) {
      return;
    }
    this.facade.loadAssignments();
    this.facade.loadFreeTables();
  }

  deleteResourceAssignment(id: string): void {
    this.facade.deleteReservationAssignment(id);
  }

  async openDetailViewReservation(reservation: IReservation) {
    const popover = await this.popoverController.create({
      component: ReservationAssignmentDetailComponent,
      componentProps: {
        reservation: reservation,
      },
      cssClass: 'full-width-popover',
      translucent: true,
    });

    return await popover.present();
  }

  async openDetailViewTable(table: ITable) {
    const popover = await this.popoverController.create({
      component: TableDetailComponent,
      componentProps: {
        table: table,
      },
      cssClass: 'full-width-popover',
      translucent: true,
    });

    return await popover.present();
  }

  ngOnDestroy(): void {
    this.facade.clearState();
    this.destroy.next();
    this.destroy.complete();
  }

  changeView(view: 'overview' | 'tables'): void {
    this.selectedView = view;
  }
}
