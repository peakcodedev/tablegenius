import { Component, Inject, OnInit } from '@angular/core';
import { MenuItem } from 'primeng/api';
import { DOCUMENT } from '@angular/common';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.scss'],
})
export class NavigationComponent implements OnInit {
  constructor(@Inject(DOCUMENT) public document: Document) {}
  items: MenuItem[] = this.getLoggedInItems(true, true);
  visible: boolean = true;

  ngOnInit() {
    // TODO
  }

  private updateVisibility(): void {
    this.visible = false;
    setTimeout(() => (this.visible = true), 0);
  }

  private getLoggedOutItems(): MenuItem[] {
    return [
      {
        label: 'Startseite',
        routerLink: '/register',
        icon: 'pi pi-home',
      },
      {
        label: 'Login',
        icon: 'pi pi-sign-in',
        routerLink: '/login',
      },
      {
        label: 'Impressum',
        routerLink: '/about',
        icon: 'pi pi-info',
      },
    ];
  }

  private getLoggedInItems(
    isAdmin: boolean,
    isSuperAdmin: boolean
  ): MenuItem[] {
    return [
      {
        label: 'Startseite',
        routerLink: '/home',
        icon: 'pi pi-home',
        visible: !isAdmin,
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
        label: 'Reservation',
        routerLink: '/users',
        icon: 'pi pi-calendar',
        visible: !isSuperAdmin,
      },
      {
        label: 'Logout',
        icon: 'pi pi-chevron-circle-right',
        command: () => {
          // TODO
        },
      },
    ];
  }
}
