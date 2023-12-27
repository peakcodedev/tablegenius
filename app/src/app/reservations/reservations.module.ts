import { NgModule } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';

import { reservationsRouting } from './reservations-routing.module';
import { ReservationsListComponent } from './components/reservations-list/reservations-list.component';
import { AddReservationComponent } from './components/add-reservation/add-reservation.component';
import { DesignModule } from '../design/design.module';
import { InputTextModule } from 'primeng/inputtext';
import { ReactiveFormsModule } from '@angular/forms';
import { NgxsFormPluginModule } from '@ngxs/form-plugin';
import { EditReservationComponent } from './components/edit-reservation/edit-reservation.component';
import { CoreModule } from '../core/core.module';
import { DropdownModule } from 'primeng/dropdown';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { ReservationCoreModule } from '../reservations-core/reservation-core.module';
import { CalendarModule } from 'primeng/calendar';

@NgModule({
  declarations: [
    ReservationsListComponent,
    AddReservationComponent,
    EditReservationComponent,
  ],
  imports: [
    CommonModule,
    reservationsRouting,
    DesignModule,
    InputTextModule,
    ReactiveFormsModule,
    NgxsFormPluginModule,
    CoreModule,
    ReservationCoreModule,
    DropdownModule,
    InputTextareaModule,
    CalendarModule,
  ],
  providers: [DatePipe],
})
export class ReservationsModule {}
