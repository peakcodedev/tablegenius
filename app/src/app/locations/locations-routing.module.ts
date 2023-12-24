import { RouterModule, Routes } from '@angular/router';
import { LocationsListComponent } from './components/locations-list/locations-list.component';
import { AddLocationComponent } from './components/add-location/add-location.component';
import { EditLocationComponent } from './components/edit-location/edit-location.component';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    component: LocationsListComponent,
  },
  {
    path: 'add',
    component: AddLocationComponent,
  },
  {
    path: ':locationId/edit',
    component: EditLocationComponent,
  },
];

export const locationsRouting = RouterModule.forChild(routes);
