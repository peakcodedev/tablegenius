import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgxsFormPluginModule } from '@ngxs/form-plugin';
import { NgxsModule } from '@ngxs/store';
import { CoreModule } from '../core/core.module';
import { ReservationState } from './state/reservation.state';
import { ReservationFacade } from './state/reservation.facade';
import { ReservationService } from './services/reservation.service';
import { CanActivateReservations } from './guards/can-activate-reservations.service';

@NgModule({
  declarations: [],
  imports: [
    NgxsModule.forFeature([ReservationState]),
    CommonModule,
    NgxsFormPluginModule,
    CoreModule,
  ],
  providers: [ReservationFacade, ReservationService, CanActivateReservations],
  exports: [],
})
export class ReservationCoreModule {}
