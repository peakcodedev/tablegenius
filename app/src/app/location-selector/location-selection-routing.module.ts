import { RouterModule, Routes } from '@angular/router';
import { LocationSelectorComponent } from './components/location-selector/location-selector.component';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    component: LocationSelectorComponent,
  },
];

export const locationSelectionRouting = RouterModule.forChild(routes);
