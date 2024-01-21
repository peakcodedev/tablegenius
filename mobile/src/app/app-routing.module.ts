import { NgModule } from '@angular/core';
import { PreloadAllModules, RouterModule, Routes } from '@angular/router';
import { AuthGuard } from '@auth0/auth0-angular';
import { TabsContainerPage } from './main/pages/tabs-container/tabs-container-page.component';
import { LoginPage } from './main/pages/login/login.page';
import { LogoutPage } from './main/pages/logout/logout.page';
import { HomePage } from './main/pages/home/home.page';
import { LoggedOutPage } from './main/pages/logged-out/logged-out-page.component';

const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: 'login', component: LoginPage },
  { path: 'logged-out', component: LoggedOutPage },
  {
    path: 'tabs',
    canActivate: [AuthGuard],
    component: TabsContainerPage,
    children: [
      {
        path: 'home',
        component: HomePage,
      },
      {
        path: 'logout',
        component: LogoutPage,
      },
      {
        path: 'reservations',
        loadChildren: () =>
          import('./reservations/reservations.module').then(
            m => m.ReservationsModule
          ),
      },
      {
        path: 'table-assignments',
        loadChildren: () =>
          import('./table-assignments/table-assignments.module').then(
            m => m.TableAssignmentsModule
          ),
      },
      {
        path: 'overview',
        loadChildren: () =>
          import(
            './reservation-assignments/reservation-assignments.module'
          ).then(m => m.ReservationAssignmentsModule),
      },
      {
        path: '',
        redirectTo: '/tabs/home',
        pathMatch: 'full',
      },
    ],
  },
  {
    path: 'intro',
    loadChildren: () =>
      import('./location-selector/location-selector.module').then(
        m => m.LocationSelectorModule
      ),
  },
];
@NgModule({
  imports: [
    RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules }),
  ],
  exports: [RouterModule],
})
export class AppRoutingModule {}
