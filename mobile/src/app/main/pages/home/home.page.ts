import { Component, OnInit } from '@angular/core';
import { LocationService } from '../../../locations-core/services/location.service';
import { map, Observable } from 'rxjs';
import { ILocation } from '../../../domain/location';
import { AppState } from '../../../core/app-state';
import { CoreState } from '../../../core/state/core.state';

@Component({
  selector: 'app-home-page',
  templateUrl: 'home.page.html',
  styleUrls: ['home.page.scss'],
})
export class HomePage implements OnInit {
  location$: Observable<ILocation>;
  constructor(
    private readonly locationService: LocationService,
    private readonly appState: AppState
  ) {}

  ngOnInit(): void {
    this.location$ = this.locationService
      .getLocation(this.appState.select(CoreState.tenantId))
      .pipe(
        map(res => {
          if (res.data) {
            return res.data;
          }
          return undefined;
        })
      );
  }
}
