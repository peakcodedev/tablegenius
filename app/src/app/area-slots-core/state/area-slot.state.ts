import { Injectable } from '@angular/core';
import { Action, Selector, State, StateContext } from '@ngxs/store';
import {
  AddAreaSlot,
  DeleteAreaSlot,
  LoadAreaSlots,
  ResetErrorMessage,
  UpdateAreaSlot,
} from './area-slot.actions';
import { AreaSlotService } from '../services/area-slot.service';
import { catchError, finalize, of, tap } from 'rxjs';
import { AreaSlotStateModel } from './area-slot-state-model';
import { ResetForm } from '@ngxs/form-plugin';
import {
  insertItem,
  patch,
  removeItem,
  updateItem,
} from '@ngxs/store/operators';
import { Navigate } from '@ngxs/router-plugin';
import { IAreaSlot } from '../../domain/area-slot';

const defaults: AreaSlotStateModel = {
  data: [],
  errorMessage: '',
  loading: false,
  editAreaSlotForm: {
    model: undefined,
    dirty: false,
    status: '',
    errors: {},
  },
  addAreaSlotForm: {
    model: undefined,
    dirty: false,
    status: '',
    errors: {},
  },
};

@State<AreaSlotStateModel>({
  name: 'areaSlots',
  defaults,
})
@Injectable()
export class AreaSlotState {
  @Selector() static errorMessage(state: AreaSlotStateModel) {
    return state.errorMessage;
  }

  @Selector() static loading(state: AreaSlotStateModel) {
    return state.loading;
  }

  @Selector() static areaSlots(state: AreaSlotStateModel) {
    return state.data;
  }

  @Selector() static areaSlotsList(state: AreaSlotStateModel) {
    return state.data.map(table => {
      return { ...table };
    });
  }

  @Selector() static areaSlot(state: AreaSlotStateModel) {
    return (id: string) => state.data.find(e => e.id === id);
  }

  constructor(private readonly areaSlotService: AreaSlotService) {}

  @Action(ResetErrorMessage)
  resetErrorMessage(
    { patchState }: StateContext<AreaSlotStateModel>,
    {}: ResetErrorMessage
  ) {
    patchState({
      errorMessage: '',
    });
  }

  @Action(LoadAreaSlots)
  loadAreaSlots(
    { patchState, dispatch }: StateContext<AreaSlotStateModel>,
    {}: LoadAreaSlots
  ) {
    patchState({ loading: true });
    return this.areaSlotService.getAreaSlots().pipe(
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

  @Action(AddAreaSlot)
  addAreaSlot(context: StateContext<AreaSlotStateModel>, {}: AddAreaSlot) {
    context.patchState({ loading: true });
    let formValues = context.getState().addAreaSlotForm.model;
    return this.areaSlotService.createAreaSlot(formValues).pipe(
      tap(res => {
        context.setState(
          patch<AreaSlotStateModel>({
            data: insertItem<IAreaSlot>(res.data),
          })
        );
        context.dispatch([
          new ResetForm({ path: 'areaSlots.addAreaSlotForm' }),
          new Navigate(['/areaSlots']),
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

  @Action(UpdateAreaSlot)
  updateAreaSlot(
    context: StateContext<AreaSlotStateModel>,
    action: UpdateAreaSlot
  ) {
    context.patchState({ loading: true });
    let formValues = context.getState().editAreaSlotForm.model;
    return this.areaSlotService
      .updateAreaSlot(action.areaSlotId, formValues)
      .pipe(
        tap(res => {
          context.setState(
            patch<AreaSlotStateModel>({
              data: updateItem<IAreaSlot>(
                table => table.id === action.areaSlotId,
                res.data
              ),
            })
          );
          context.dispatch([
            new ResetForm({ path: 'areaSlots.editAreaSlotForm' }),
            new Navigate(['/areaSlots']),
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

  @Action(DeleteAreaSlot)
  deleteAreaSlot(
    context: StateContext<AreaSlotStateModel>,
    action: DeleteAreaSlot
  ) {
    context.patchState({ loading: true });
    return this.areaSlotService.deleteAreaSlot(action.areaSlotId).pipe(
      tap(_ => {
        context.setState(
          patch<AreaSlotStateModel>({
            data: removeItem<IAreaSlot>(
              table => table.id === action.areaSlotId
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
