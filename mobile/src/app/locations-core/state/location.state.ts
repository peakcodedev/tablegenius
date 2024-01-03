import { Injectable } from '@angular/core';
import { Action, Selector, State, StateContext } from '@ngxs/store';
import {
  AddLocation,
  DeleteLocation,
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

const defaults: LocationStateModel = {
  data: [],
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

  @Selector() static locationsList(state: LocationStateModel) {
    return state.data.map(location => {
      return { ...location };
    });
  }

  @Selector() static location(state: LocationStateModel) {
    return (id: string) => state.data.find(e => e.id === id);
  }

  constructor(private readonly locationService: LocationService) {}

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
        patchState({ data: response.data, loading: false });
        dispatch(new ResetErrorMessage());
      }),
      catchError(error => {
        patchState({ errorMessage: error, loading: false });
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
}
