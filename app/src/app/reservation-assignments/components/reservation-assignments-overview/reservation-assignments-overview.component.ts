import { Component, OnDestroy, OnInit } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { AreasHelper } from '../../../areas-core/helpers/areas.helper';
import { DropdownChangeEvent } from 'primeng/dropdown';
import { AreaFacade } from '../../../areas-core/state/area.facade';
import { AreaSlotsHelper } from '../../../area-slots-core/helpers/area-slots.helper';
import { ReservationAssignmentFacade } from '../../../reservation-assignments-core/state/reservation-assignment.facade';

@Component({
  selector: 'reservation-assignments-overview',
  templateUrl: './reservation-assignments-overview.component.html',
  styleUrls: ['./reservation-assignments-overview.component.scss'],
})
export class ReservationAssignmentsOverviewComponent
  implements OnInit, OnDestroy
{
  minDate = new Date();
  areas: Observable<any[]>;
  areaSlots: any[];
  selectedArea = '';
  selectedAreaSlot = '';
  selectedDate: Date = null;

  constructor(
    private readonly areasHelper: AreasHelper,
    readonly facade: ReservationAssignmentFacade,
    readonly areaFacade: AreaFacade,
    private readonly areaSlotsHelper: AreaSlotsHelper
  ) {}

  ngOnInit(): void {
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
          if (value) {
            this.loadData();
          }
        })
      )
      .subscribe();
  }

  onDateSelect(date: Date): void {
    this.facade.setSelectedDate(date);
    this.facade.setSelectedArea(undefined);
    this.facade.setSelectedAreaSlot(undefined);
  }

  onAreaSelect(event: DropdownChangeEvent): void {
    this.facade.setSelectedArea(event.value);
    this.facade.setSelectedAreaSlot(undefined);
  }

  onAreaSlotSelect(event: DropdownChangeEvent): void {
    this.facade.setSelectedAreaSlot(event.value);
  }

  loadData(): void {
    this.facade.loadAssignments();
    this.facade.loadFreeTables();
  }

  deleteResourceAssignment(id: string): void {
    this.facade.deleteReservationAssignment(id);
  }

  ngOnDestroy(): void {
    this.facade.clearState();
  }
}
