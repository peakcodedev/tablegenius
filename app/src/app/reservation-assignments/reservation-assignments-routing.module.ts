import { RouterModule, Routes } from '@angular/router';
import { ReservationAssignmentsOverviewComponent } from './components/reservation-assignments-overview/reservation-assignments-overview.component';
import { CanActivateAreas } from '../areas-core/guards/can-activate-areas.service';
import { CanActivateAreaSlots } from '../area-slots-core/guards/can-activate-area-slots.service';

const routes: Routes = [
  {
    path: 'overview',
    component: ReservationAssignmentsOverviewComponent,
    canActivate: [CanActivateAreas, CanActivateAreaSlots],
  },
];

export const reservationAssignmentsRouting = RouterModule.forChild(routes);
