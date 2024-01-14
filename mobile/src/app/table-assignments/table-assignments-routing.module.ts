import { RouterModule, Routes } from '@angular/router';
import { TableAssignmentsOverviewComponent } from './components/table-assignments-overview/table-assignments-overview.component';
import { CanActivateAreas } from '../areas-core/guards/can-activate-areas.service';
import { CanActivateAreaSlots } from '../area-slots-core/guards/can-activate-area-slots.service';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    component: TableAssignmentsOverviewComponent,
    canActivate: [CanActivateAreas, CanActivateAreaSlots],
  },
];

export const tableAssignmentsRouting = RouterModule.forChild(routes);
