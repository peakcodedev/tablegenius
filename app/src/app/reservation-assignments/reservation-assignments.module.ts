import { NgModule } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';

import { reservationAssignmentsRouting } from './reservation-assignments-routing.module';
import { ReservationAssignmentsOverviewComponent } from './components/reservation-assignments-overview/reservation-assignments-overview.component';
import { DesignModule } from '../design/design.module';
import { InputTextModule } from 'primeng/inputtext';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgxsFormPluginModule } from '@ngxs/form-plugin';
import { CoreModule } from '../core/core.module';
import { DropdownModule } from 'primeng/dropdown';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { CalendarModule } from 'primeng/calendar';
import { AreasCoreModule } from '../areas-core/areas-core.module';
import { AreaSlotsCoreModule } from '../area-slots-core/area-slots-core.module';
import { ReservationAssignmentCoreModule } from '../reservation-assignments-core/reservation-assignment-core.module';

@NgModule({
  declarations: [ReservationAssignmentsOverviewComponent],
  imports: [
    CommonModule,
    reservationAssignmentsRouting,
    DesignModule,
    InputTextModule,
    FormsModule,
    ReactiveFormsModule,
    NgxsFormPluginModule,
    CoreModule,
    DropdownModule,
    InputTextareaModule,
    AreasCoreModule,
    AreaSlotsCoreModule,
    CalendarModule,
    ReservationAssignmentCoreModule,
  ],
  providers: [DatePipe],
})
export class ReservationAssignmentsModule {}
