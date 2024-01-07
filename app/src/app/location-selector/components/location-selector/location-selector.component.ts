import { Component, OnInit } from '@angular/core';
import { LocationSelectionFacade } from '../../state/location-selection.facade';
import { CoreFacade } from '../../../core/state/core.facade';
import { filter } from 'rxjs';
import { Router } from '@angular/router';
import { AuthenticationService } from '../../../core/services/authentication.service';

@Component({
  selector: 'app-location-selector',
  templateUrl: './location-selector.component.html',
  styleUrl: './location-selector.component.scss',
})
export class LocationSelectorComponent implements OnInit {
  constructor(
    readonly facade: LocationSelectionFacade,
    private readonly coreFacade: CoreFacade,
    private readonly router: Router,
    private readonly authService: AuthenticationService
  ) {}

  onClick(event: Event, locationId: string): void {
    event.stopImmediatePropagation();
    this.facade.setLocation(locationId);
  }

  ngOnInit(): void {
    this.authService
      .isSuperAdmin()
      .pipe(filter(isSuperAdmin => isSuperAdmin))
      .subscribe(_ => {
        this.coreFacade.setTenantId('superAdmin');
      });
    this.facade.loadLocationsOfUser();
    this.coreFacade.tenantId.pipe(filter(Boolean)).subscribe(value => {
      this.router.navigate(['home']);
    });
  }
}
