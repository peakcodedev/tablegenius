import { NgModule } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';

import { reservationAssignmentsRouting } from './reservation-assignments-routing.module';
import { ReservationAssignmentsOverviewComponent } from './components/reservation-assignments-overview/reservation-assignments-overview.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgxsFormPluginModule } from '@ngxs/form-plugin';
import { CoreModule } from '../core/core.module';
import { AreasCoreModule } from '../areas-core/areas-core.module';
import { AreaSlotsCoreModule } from '../area-slots-core/area-slots-core.module';
import { ReservationAssignmentCoreModule } from '../reservation-assignments-core/reservation-assignment-core.module';
import { IonicModule } from '@ionic/angular';
import { ReservationAssignmentDetailComponent } from './components/reservation-assignment-detail/reservation-assignment-detail.component';
import { TableDetailComponent } from './components/table-detail/table-detail.component';

@NgModule({
  declarations: [
    ReservationAssignmentsOverviewComponent,
    ReservationAssignmentDetailComponent,
    TableDetailComponent,
  ],
  imports: [
    CommonModule,
    reservationAssignmentsRouting,
    FormsModule,
    ReactiveFormsModule,
    NgxsFormPluginModule,
    CoreModule,
    AreasCoreModule,
    AreaSlotsCoreModule,
    ReservationAssignmentCoreModule,
    IonicModule,
  ],
  providers: [DatePipe],
})
export class ReservationAssignmentsModule {}
