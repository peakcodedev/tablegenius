import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgxsFormPluginModule } from '@ngxs/form-plugin';
import { NgxsModule } from '@ngxs/store';
import { CoreModule } from '../core/core.module';
import { ReservationAssignmentState } from './state/reservation-assignment.state';
import { ReservationAssignmentFacade } from './state/reservation-assignment.facade';
import { ReservationAssignmentsService } from './services/reservation-assignments.service';
import { CanActivateReservationAssignments } from './guards/can-activate-reservation-assignments.service';

@NgModule({
  declarations: [],
  imports: [
    NgxsModule.forFeature([ReservationAssignmentState]),
    CommonModule,
    NgxsFormPluginModule,
    CoreModule,
  ],
  providers: [
    ReservationAssignmentFacade,
    ReservationAssignmentsService,
    CanActivateReservationAssignments,
  ],
  exports: [],
})
export class ReservationAssignmentCoreModule {}
