import { Injectable } from '@angular/core';
import { Action, Selector, State, StateContext } from '@ngxs/store';
import {
  AddLocation,
  AddLocationAssignment,
  DeleteLocation,
  DeleteLocationAssignment,
  LoadLocationAssignments,
  LoadLocations,
  ResetErrorMessage,
  UpdateLocation,
} from './location.actions';
import { LocationService } from '../services/location.service';
import { catchError, finalize, of, tap } from 'rxjs';
import { LocationStateModel } from './location-state-model';
import { ResetForm } from '@ngxs/form-plugin';
import {
  insertItem,
  patch,
  removeItem,
  updateItem,
} from '@ngxs/store/operators';
import { Navigate } from '@ngxs/router-plugin';
import { ILocation } from '../../domain/location';
import { LocationAssignmentService } from '../services/location-assignment.service';
import { ILocationAssignment } from '../../domain/location-assignment';

const defaults: LocationStateModel = {
  data: [],
  assignments: [],
  errorMessage: '',
  loading: false,
  editLocationForm: {
    model: undefined,
    dirty: false,
    status: '',
    errors: {},
  },
  addLocationForm: {
    model: undefined,
    dirty: false,
    status: '',
    errors: {},
  },
  addLocationAssignmentForm: {
    model: undefined,
    dirty: false,
    status: '',
    errors: {},
  },
};

@State<LocationStateModel>({
  name: 'locations',
  defaults,
})
@Injectable()
export class LocationState {
  @Selector() static errorMessage(state: LocationStateModel) {
    return state.errorMessage;
  }

  @Selector() static loading(state: LocationStateModel) {
    return state.loading;
  }

  @Selector() static locations(state: LocationStateModel) {
    return state.data;
  }

  @Selector() static assignments(state: LocationStateModel) {
    return state.assignments;
  }

  @Selector() static assignmentsList(state: LocationStateModel) {
    return state.assignments.map(assignment => {
      return { ...assignment };
    });
  }

  @Selector() static locationsList(state: LocationStateModel) {
    return state.data.map(location => {
      return { ...location };
    });
  }

  @Selector() static location(state: LocationStateModel) {
    return (id: string) => state.data.find(e => e.id === id);
  }

  constructor(
    private readonly locationService: LocationService,
    private readonly locationAssignmentService: LocationAssignmentService
  ) {}

  @Action(ResetErrorMessage)
  resetErrorMessage(
    { patchState }: StateContext<LocationStateModel>,
    {}: ResetErrorMessage
  ) {
    patchState({
      errorMessage: '',
    });
  }

  @Action(LoadLocations)
  loadLocations(
    { patchState, dispatch }: StateContext<LocationStateModel>,
    {}: LoadLocations
  ) {
    patchState({ loading: true });
    return this.locationService.getLocations().pipe(
      tap(response => {
        patchState({ data: response.data, loading: false, assignments: [] });
        dispatch(new ResetErrorMessage());
      }),
      catchError(error => {
        patchState({ errorMessage: error, loading: false, assignments: [] });
        return of([]);
      })
    );
  }

  @Action(LoadLocationAssignments)
  loadLocationAssignments(
    { patchState, dispatch }: StateContext<LocationStateModel>,
    action: LoadLocationAssignments
  ) {
    patchState({ loading: true });
    return this.locationAssignmentService
      .getLocationAssignments(action.locationId)
      .pipe(
        tap(response => {
          patchState({ assignments: response.data, loading: false });
          dispatch(new ResetErrorMessage());
        }),
        catchError(error => {
          patchState({ errorMessage: error, loading: false, data: [] });
          return of([]);
        })
      );
  }

  @Action(AddLocation)
  addLocation(context: StateContext<LocationStateModel>, {}: AddLocation) {
    context.patchState({ loading: true });
    let formValues = context.getState().addLocationForm.model;
    return this.locationService.createLocation(formValues).pipe(
      tap(res => {
        context.setState(
          patch<LocationStateModel>({
            data: insertItem<ILocation>(res.data),
          })
        );
        context.dispatch([
          new ResetForm({ path: 'locations.addLocationForm' }),
          new Navigate(['/locations']),
        ]);
      }),
      catchError(error => {
        context.patchState({ errorMessage: error, loading: false });
        return of([]);
      }),
      finalize(() => {
        context.patchState({ loading: false });
      })
    );
  }

  @Action(AddLocationAssignment)
  addLocationAssignment(
    context: StateContext<LocationStateModel>,
    _: AddLocationAssignment
  ) {
    context.patchState({ loading: true });
    let formValues = context.getState().addLocationAssignmentForm.model;
    return this.locationAssignmentService
      .createLocationAssignment(formValues)
      .pipe(
        tap(res => {
          context.setState(
            patch<LocationStateModel>({
              assignments: insertItem<ILocationAssignment>(res.data),
            })
          );
          context.dispatch([
            new ResetForm({ path: 'locations.addLocationAssignmentForm' }),
            new Navigate(['/locations']),
          ]);
        }),
        catchError(error => {
          context.patchState({ errorMessage: error, loading: false });
          return of([]);
        }),
        finalize(() => {
          context.patchState({ loading: false });
        })
      );
  }

  @Action(UpdateLocation)
  updateLocation(
    context: StateContext<LocationStateModel>,
    action: UpdateLocation
  ) {
    context.patchState({ loading: true });
    let formValues = context.getState().editLocationForm.model;
    return this.locationService
      .updateLocation(action.locationId, formValues)
      .pipe(
        tap(res => {
          context.setState(
            patch<LocationStateModel>({
              data: updateItem<ILocation>(
                location => location.id === action.locationId,
                res.data
              ),
            })
          );
          context.dispatch([
            new ResetForm({ path: 'locations.editLocationForm' }),
            new Navigate(['/locations']),
          ]);
        }),
        catchError(error => {
          context.patchState({ errorMessage: error, loading: false });
          return of([]);
        }),
        finalize(() => {
          context.patchState({ loading: false });
        })
      );
  }

  @Action(DeleteLocation)
  deleteLocation(
    context: StateContext<LocationStateModel>,
    action: DeleteLocation
  ) {
    context.patchState({ loading: true });
    return this.locationService.deleteLocation(action.locationId).pipe(
      tap(_ => {
        context.setState(
          patch<LocationStateModel>({
            data: removeItem<ILocation>(
              location => location.id === action.locationId
            ),
          })
        );
      }),
      catchError(error => {
        context.patchState({ errorMessage: error, loading: false });
        return of([]);
      }),
      finalize(() => {
        context.patchState({ loading: false });
      })
    );
  }

  @Action(DeleteLocationAssignment)
  deleteLocationAssignment(
    context: StateContext<LocationStateModel>,
    action: DeleteLocationAssignment
  ) {
    context.patchState({ loading: true });
    return this.locationAssignmentService
      .deleteLocationAssignment(action.locationAssignmentId)
      .pipe(
        tap(_ => {
          context.setState(
            patch<LocationStateModel>({
              assignments: removeItem<ILocationAssignment>(
                locationAssignment =>
                  locationAssignment.id === action.locationAssignmentId
              ),
            })
          );
        }),
        catchError(error => {
          context.patchState({ errorMessage: error, loading: false });
          return of([]);
        }),
        finalize(() => {
          context.patchState({ loading: false });
        })
      );
  }
}
