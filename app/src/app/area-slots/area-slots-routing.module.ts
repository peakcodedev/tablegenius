import { RouterModule, Routes } from '@angular/router';
import { AreaSlotsListComponent } from './components/area-slots-list/area-slots-list.component';
import { AddAreaSlotComponent } from './components/add-area-slot/add-area-slot.component';
import { EditAreaSlotComponent } from './components/edit-area-slot/edit-area-slot.component';
import { CanActivateAreas } from '../areas-core/guards/can-activate-areas.service';

const routes: Routes = [
  {
    path: '',
    canActivate: [CanActivateAreas],
    children: [
      {
        path: '',
        pathMatch: 'full',
        component: AreaSlotsListComponent,
      },
      {
        path: 'add',
        component: AddAreaSlotComponent,
      },
      {
        path: ':areaSlotId',
        component: EditAreaSlotComponent,
      },
    ],
  },
];

export const areaSlotsRouting = RouterModule.forChild(routes);
