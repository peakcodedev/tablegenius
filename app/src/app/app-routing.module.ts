import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: 'tables',
    //canActivate: [AuthGuard],
    loadChildren: () =>
      import('./tables/tables.module').then(m => m.TablesModule),
  },
  {
    path: 'areas',
    //canActivate: [AuthGuard],
    loadChildren: () => import('./areas/areas.module').then(m => m.AreasModule),
  },
  {
    path: 'locations',
    //canActivate: [AuthGuard],
    loadChildren: () =>
      import('./locations/locations.module').then(m => m.LocationsModule),
  },
  {
    path: 'reservations',
    //canActivate: [AuthGuard],
    loadChildren: () =>
      import('./reservations/reservations.module').then(
        m => m.ReservationsModule
      ),
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
