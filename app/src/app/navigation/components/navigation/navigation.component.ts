import { Component, Inject, OnInit } from '@angular/core';
import { MenuItem } from 'primeng/api';
import { DOCUMENT } from '@angular/common';
import { CoreFacade } from '../../../core/state/core.facade';
import { catchError, filter, of, tap } from 'rxjs';
import {
  AppState,
  AuthService,
  RedirectLoginOptions,
} from '@auth0/auth0-angular';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.scss'],
})
export class NavigationComponent implements OnInit {
  constructor(
    @Inject(DOCUMENT) public document: Document,
    readonly coreFacade: CoreFacade,
    private readonly authService: AuthService
  ) {}
  items: MenuItem[] = this.getLoggedOutItems();
  visible: boolean = true;

  ngOnInit() {
    this.authService.isAuthenticated$
      .pipe(
        filter(isAuthenticated => Boolean(isAuthenticated)),
        tap(isAuthenticated => {
          if (isAuthenticated) {
            this.items = this.getLoggedInItems(true, false);
          } else {
            this.items = this.getLoggedOutItems();
          }
        }),
        catchError(_ => {
          this.items = this.getLoggedOutItems();
          return of(null);
        })
      )
      .subscribe(_ => {
        this.updateVisibility();
      });
  }

  private updateVisibility(): void {
    this.visible = false;
    setTimeout(() => (this.visible = true), 0);
  }

  private getLoggedOutItems(): MenuItem[] {
    return [
      {
        label: 'Startseite',
        routerLink: '/info',
        icon: 'pi pi-home',
      },
      {
        label: 'Login',
        icon: 'pi pi-sign-in',
        routerLink: '/login',
        command: () => {
          this.authService.loginWithRedirect(this.getLoginOptions());
        },
      },
    ];
  }

  private getLoggedInItems(
    isAdmin: boolean,
    isSuperAdmin: boolean
  ): MenuItem[] {
    return [
      {
        routerLink: '/home',
        icon: 'pi pi-home',
      },
      {
        label: 'Restaurants',
        routerLink: '/locations',
        icon: 'pi pi-chevron-circle-right',
        visible: isSuperAdmin,
      },
      {
        label: 'Bereiche',
        routerLink: '/areas',
        icon: 'pi pi-users',
        visible: isAdmin,
      },
      {
        label: 'Tische',
        routerLink: '/tables',
        icon: 'pi pi-shopping-cart',
        visible: isAdmin,
      },
      {
        label: 'Zeitbereiche',
        routerLink: '/areaSlots',
        icon: 'pi pi-shopping-cart',
        visible: isAdmin,
      },
      {
        label: 'Zuweisungen',
        routerLink: '/tableAssignments',
        icon: 'pi pi-shopping-cart',
        visible: isAdmin,
      },
      {
        label: 'Reservation',
        routerLink: '/reservations',
        icon: 'pi pi-calendar',
        visible: !isSuperAdmin,
      },
      {
        label: 'Logout',
        icon: 'pi pi-chevron-circle-right',
        command: () => {
          this.authService.logout({
            logoutParams: { returnTo: document.location.origin + '/logout' },
          });
          this.items = this.getLoggedOutItems();
          this.updateVisibility();
          this.coreFacade.setTenantId('');
        },
      },
    ];
  }

  getLoginOptions(): RedirectLoginOptions<AppState> {
    return {
      appState: { target: '/intro' },
    };
  }
}
