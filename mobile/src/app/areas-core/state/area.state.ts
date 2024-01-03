import { Injectable } from '@angular/core';
import { Action, Selector, State, StateContext } from '@ngxs/store';
import {
  AddArea,
  DeleteArea,
  LoadAreas,
  ResetErrorMessage,
  UpdateArea,
} from './area.actions';
import { AreaService } from '../services/area.service';
import { catchError, finalize, of, tap } from 'rxjs';
import { AreaStateModel } from './area-state-model';
import { ResetForm } from '@ngxs/form-plugin';
import {
  insertItem,
  patch,
  removeItem,
  updateItem,
} from '@ngxs/store/operators';
import { Navigate } from '@ngxs/router-plugin';
import { IArea } from '../../domain/area';

const defaults: AreaStateModel = {
  data: [],
  errorMessage: '',
  loading: false,
  editAreaForm: {
    model: undefined,
    dirty: false,
    status: '',
    errors: {},
  },
  addAreaForm: {
    model: undefined,
    dirty: false,
    status: '',
    errors: {},
  },
};

@State<AreaStateModel>({
  name: 'areas',
  defaults,
})
@Injectable()
export class AreaState {
  @Selector() static errorMessage(state: AreaStateModel) {
    return state.errorMessage;
  }

  @Selector() static loading(state: AreaStateModel) {
    return state.loading;
  }

  @Selector() static areas(state: AreaStateModel) {
    return state.data;
  }

  @Selector() static areasList(state: AreaStateModel) {
    return state.data.map(area => {
      return { ...area };
    });
  }

  @Selector() static area(state: AreaStateModel) {
    return (id: string) => state.data.find(e => e.id === id);
  }

  constructor(private readonly areaService: AreaService) {}

  @Action(ResetErrorMessage)
  resetErrorMessage(
    { patchState }: StateContext<AreaStateModel>,
    {}: ResetErrorMessage
  ) {
    patchState({
      errorMessage: '',
    });
  }

  @Action(LoadAreas)
  loadAreas(
    { patchState, dispatch }: StateContext<AreaStateModel>,
    {}: LoadAreas
  ) {
    patchState({ loading: true });
    return this.areaService.getAreas().pipe(
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

  @Action(AddArea)
  addArea(context: StateContext<AreaStateModel>, {}: AddArea) {
    context.patchState({ loading: true });
    let formValues = context.getState().addAreaForm.model;
    return this.areaService.createArea(formValues).pipe(
      tap(res => {
        context.setState(
          patch<AreaStateModel>({
            data: insertItem<IArea>(res.data),
          })
        );
        context.dispatch([
          new ResetForm({ path: 'areas.addAreaForm' }),
          new Navigate(['/areas']),
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

  @Action(UpdateArea)
  updateArea(context: StateContext<AreaStateModel>, action: UpdateArea) {
    context.patchState({ loading: true });
    let formValues = context.getState().editAreaForm.model;
    return this.areaService.updateArea(action.areaId, formValues).pipe(
      tap(res => {
        context.setState(
          patch<AreaStateModel>({
            data: updateItem<IArea>(
              expert => expert.id === action.areaId,
              res.data
            ),
          })
        );
        context.dispatch([
          new ResetForm({ path: 'areas.editAreaForm' }),
          new Navigate(['/areas']),
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

  @Action(DeleteArea)
  deleteArea(context: StateContext<AreaStateModel>, action: DeleteArea) {
    context.patchState({ loading: true });
    return this.areaService.deleteArea(action.areaId).pipe(
      tap(_ => {
        context.setState(
          patch<AreaStateModel>({
            data: removeItem<IArea>(area => area.id === action.areaId),
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
