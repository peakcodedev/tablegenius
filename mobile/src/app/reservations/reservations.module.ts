import { NgModule } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';

import { reservationsRouting } from './reservations-routing.module';
import { ReservationsListComponent } from './components/reservations-list/reservations-list.component';
import { AddReservationComponent } from './components/add-reservation/add-reservation.component';
import { ReactiveFormsModule } from '@angular/forms';
import { NgxsFormPluginModule } from '@ngxs/form-plugin';
import { EditReservationComponent } from './components/edit-reservation/edit-reservation.component';
import { CoreModule } from '../core/core.module';
import { ReservationCoreModule } from '../reservations-core/reservation-core.module';
import { IonicModule } from '@ionic/angular';

@NgModule({
  declarations: [
    ReservationsListComponent,
    AddReservationComponent,
    EditReservationComponent,
  ],
  imports: [
    CommonModule,
    reservationsRouting,
    ReactiveFormsModule,
    NgxsFormPluginModule,
    CoreModule,
    ReservationCoreModule,
    CommonModule,
    IonicModule,
  ],
  providers: [DatePipe],
})
export class ReservationsModule {}
