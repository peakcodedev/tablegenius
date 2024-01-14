import { Injectable } from '@angular/core';
import { Action, Selector, State, StateContext } from '@ngxs/store';
import {
  LoadLocationsOfUser,
  ResetErrorMessage,
  SetLocation,
} from './location-selection.actions';
import { UserLocationsService } from '../services/user-locations.service';
import { catchError, of, tap } from 'rxjs';
import { LocationSelectionStateModel } from './location-selection-state-model';
import { SetTenantId } from '../../core/state/core.actions';

const defaults: LocationSelectionStateModel = {
  locations: [],
  errorMessage: '',
  loading: false,
};

@State<LocationSelectionStateModel>({
  name: 'locationSelection',
  defaults,
})
@Injectable()
export class LocationSelectionState {
  @Selector() static errorMessage(state: LocationSelectionStateModel) {
    return state.errorMessage;
  }

  @Selector() static loading(state: LocationSelectionStateModel) {
    return state.loading;
  }

  @Selector() static locations(state: LocationSelectionStateModel) {
    return state.locations;
  }

  constructor(private readonly userLocationsService: UserLocationsService) {}

  @Action(ResetErrorMessage)
  resetErrorMessage(
    { patchState }: StateContext<LocationSelectionStateModel>,
    {}: ResetErrorMessage
  ) {
    patchState({
      errorMessage: '',
    });
  }

  @Action(LoadLocationsOfUser)
  loadLocationsOfUser(
    { patchState, dispatch }: StateContext<LocationSelectionStateModel>,
    {}: LoadLocationsOfUser
  ) {
    patchState({ loading: true });
    return this.userLocationsService.getLocations().pipe(
      tap(response => {
        patchState({ locations: response.data, loading: false });
        dispatch(new ResetErrorMessage());
      }),
      catchError(error => {
        patchState({ errorMessage: error, loading: false });
        return of([]);
      })
    );
  }

  @Action(SetLocation)
  setLocation(
    context: StateContext<LocationSelectionStateModel>,
    action: SetLocation
  ) {
    context.dispatch(new SetTenantId(action.locationId));
  }
}
