import { RouterModule, Routes } from '@angular/router';
import { TablesListComponent } from './components/tables-list/tables-list.component';
import { AddTableComponent } from './components/add-table/add-table.component';
import { EditTableComponent } from './components/edit-table/edit-table.component';
import { CanActivateAreas } from '../areas-core/guards/can-activate-areas.service';

const routes: Routes = [
  {
    path: '',
    canActivate: [CanActivateAreas],
    children: [
      {
        path: '',
        pathMatch: 'full',
        component: TablesListComponent,
      },
      {
        path: 'add',
        component: AddTableComponent,
      },
      {
        path: ':tableId',
        component: EditTableComponent,
      },
    ],
  },
];

export const tablesRouting = RouterModule.forChild(routes);
