import { Injectable } from '@angular/core';
import { Dispatch } from '@ngxs-labs/dispatch-decorator';
import { Select } from '@ngxs/store';
import { Observable } from 'rxjs';
import { LocationSelectionState } from './location-selection.state';
import { LoadLocationsOfUser, SetLocation } from './location-selection.actions';
import { AppState } from '../../core/app-state';
import { ILocation } from '../../domain/location';

@Injectable()
export class LocationSelectionFacade {
  @Select(LocationSelectionState.locations)
  locations: Observable<ILocation[]>;
  @Select(LocationSelectionState.loading)
  loading: Observable<boolean>;
  @Select(LocationSelectionState.errorMessage)
  errorMessage: Observable<string>;

  constructor(private readonly appState: AppState) {}

  @Dispatch()
  loadLocationsOfUser = () => new LoadLocationsOfUser();
  loadLocationsOfUser$ = () =>
    this.appState.dispatch(new LoadLocationsOfUser());

  @Dispatch()
  setLocation = (locationId: string) => new SetLocation(locationId);
}
