import { Injectable } from '@angular/core';
import { Dispatch } from '@ngxs-labs/dispatch-decorator';
import { Select } from '@ngxs/store';
import { map, Observable } from 'rxjs';
import { LocationState } from './location.state';
import {
  AddLocation,
  AddLocationAssignment,
  DeleteLocation,
  DeleteLocationAssignment,
  LoadLocationAssignments,
  LoadLocations,
  UpdateLocation,
} from './location.actions';
import { AppState } from '../../core/app-state';
import { ILocation } from '../../domain/location';
import { ILocationAssignment } from '../../domain/location-assignment';

@Injectable()
export class LocationFacade {
  @Select(LocationState.locations)
  locations: Observable<ILocation[]>;
  @Select(LocationState.locationsList)
  locationsList: Observable<ILocation[]>;
  @Select(LocationState.assignmentsList)
  assignmentsList: Observable<ILocationAssignment[]>;
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
  @Dispatch()
  loadLocationAssignments = (locationId: string) =>
    new LoadLocationAssignments(locationId);
  loadLocations$ = () => this.appState.dispatch(new LoadLocations());

  @Dispatch()
  addLocation = () => new AddLocation();
  @Dispatch()
  addLocationAssignment = () => new AddLocationAssignment();

  @Dispatch()
  deleteLocation = (locationId: string) => new DeleteLocation(locationId);
  @Dispatch()
  deleteLocationAssignment = (locationAssignmentId: string) =>
    new DeleteLocationAssignment(locationAssignmentId);

  @Dispatch()
  updateLocation = (locationId: string) => new UpdateLocation(locationId);
}
