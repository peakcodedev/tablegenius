import { Component, OnInit } from '@angular/core';
import { LocationSelectionFacade } from '../../state/location-selection.facade';
import { CoreFacade } from '../../../core/state/core.facade';
import { filter } from 'rxjs';
import { Router } from '@angular/router';

@Component({
  selector: 'app-location-selector',
  templateUrl: './location-selector.component.html',
  styleUrl: './location-selector.component.scss',
})
export class LocationSelectorComponent implements OnInit {
  constructor(
    readonly facade: LocationSelectionFacade,
    private readonly coreFacade: CoreFacade,
    private readonly router: Router
  ) {}

  onClick(event: Event, locationId: string): void {
    event.stopImmediatePropagation();
    console.error(locationId);
    this.facade.setLocation(locationId);
  }

  ngOnInit(): void {
    this.facade.loadLocationsOfUser();
    this.coreFacade.tenantId.pipe(filter(Boolean)).subscribe(value => {
      console.error(value);
      this.router.navigate(['home']);
    });
  }
}
