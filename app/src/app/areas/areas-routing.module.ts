import { RouterModule, Routes } from '@angular/router';
import { AreasListComponent } from './components/areas-list/areas-list.component';
import { AddAreaComponent } from './components/add-area/add-area.component';
import { EditAreaComponent } from './components/edit-area/edit-area.component';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    component: AreasListComponent,
  },
  {
    path: 'add',
    component: AddAreaComponent,
  },
  {
    path: ':areaId/edit',
    component: EditAreaComponent,
  },
];

export const areasRouting = RouterModule.forChild(routes);
