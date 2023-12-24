import { RouterModule, Routes } from '@angular/router';
import { ReservationsListComponent } from './components/reservations-list/reservations-list.component';
import { AddReservationComponent } from './components/add-reservation/add-reservation.component';
import { EditReservationComponent } from './components/edit-reservation/edit-reservation.component';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    component: ReservationsListComponent,
  },
  {
    path: 'add',
    component: AddReservationComponent,
  },
  {
    path: ':reservationId/edit',
    component: EditReservationComponent,
  },
];

export const reservationsRouting = RouterModule.forChild(routes);
