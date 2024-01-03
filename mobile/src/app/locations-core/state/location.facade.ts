import { Injectable } from '@angular/core';
import { Dispatch } from '@ngxs-labs/dispatch-decorator';
import { Select } from '@ngxs/store';
import { map, Observable } from 'rxjs';
import { LocationState } from './location.state';
import {
  AddLocation,
  DeleteLocation,
  LoadLocations,
  UpdateLocation,
} from './location.actions';
import { AppState } from '../../core/app-state';
import { ILocation } from '../../domain/location';

@Injectable()
export class LocationFacade {
  @Select(LocationState.locations)
  locations: Observable<ILocation[]>;
  @Select(LocationState.locationsList)
  locationsList: Observable<ILocation[]>;
  @Select(LocationState.loading)
  loading: Observable<boolean>;
  @Select(LocationState.errorMessage)
  errorMessage: Observable<string>;

  constructor(private readonly appState: AppState) {}

  byId$(id: string) {
    return this.appState
      .select$(LocationState.location)
      .pipe(map(selector => selector(id)));
  }

  @Dispatch()
  loadLocations = () => new LoadLocations();
  loadLocations$ = () => this.appState.dispatch(new LoadLocations());

  @Dispatch()
  addLocation = () => new AddLocation();

  @Dispatch()
  deleteLocation = (locationId: string) => new DeleteLocation(locationId);

  @Dispatch()
  updateLocation = (locationId: string) => new UpdateLocation(locationId);
}
