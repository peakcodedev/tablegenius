import { RouterModule, Routes } from '@angular/router';
import { LocationsListComponent } from './components/locations-list/locations-list.component';
import { AddLocationComponent } from './components/add-location/add-location.component';
import { EditLocationComponent } from './components/edit-location/edit-location.component';
import { LocationAssignmentsListComponent } from './components/location-assignments-list/location-assignments-list.component';
import { AddLocationAssignmentComponent } from './components/add-location-assignment/add-location-assignment.component';

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
  {
    path: ':locationId/assignments',
    component: LocationAssignmentsListComponent,
  },
  {
    path: ':locationId/assignments/add',
    component: AddLocationAssignmentComponent,
  },
];

export const locationsRouting = RouterModule.forChild(routes);
