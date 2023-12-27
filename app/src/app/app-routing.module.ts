import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from '@auth0/auth0-angular';
import { LoginPage } from './main/main/pages/login/login.page';
import { LogoutPage } from './main/main/pages/logout/logout.page';
import { HomePage } from './main/main/pages/home/home.page';

const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: 'login', component: LoginPage },
  { path: 'logout', component: LogoutPage },
  { path: 'home', component: HomePage, canActivate: [AuthGuard] },
  {
    path: 'intro',
    canActivate: [AuthGuard],
    loadChildren: () =>
      import('./location-selector/location-selector.module').then(
        m => m.LocationSelectorModule
      ),
  },
  {
    path: 'tables',
    canActivate: [AuthGuard],
    loadChildren: () =>
      import('./tables/tables.module').then(m => m.TablesModule),
  },
  {
    path: 'areas',
    canActivate: [AuthGuard],
    loadChildren: () => import('./areas/areas.module').then(m => m.AreasModule),
  },
  {
    path: 'locations',
    canActivate: [AuthGuard],
    loadChildren: () =>
      import('./locations/locations.module').then(m => m.LocationsModule),
  },
  {
    path: 'reservations',
    canActivate: [AuthGuard],
    loadChildren: () =>
      import('./reservations/reservations.module').then(
        m => m.ReservationsModule
      ),
  },
  {
    path: 'areaSlots',
    canActivate: [AuthGuard],
    loadChildren: () =>
      import('./area-slots/area-slots.module').then(m => m.AreaSlotsModule),
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
