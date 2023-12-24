import { Injectable } from '@angular/core';
import { Action, Selector, State, StateContext } from '@ngxs/store';
import {
  AddTable,
  DeleteTable,
  LoadTables,
  ResetErrorMessage,
  UpdateTable,
} from './table.actions';
import { TableService } from '../services/table.service';
import { catchError, finalize, of, tap } from 'rxjs';
import { TableStateModel } from './table-state-model';
import { ResetForm } from '@ngxs/form-plugin';
import {
  insertItem,
  patch,
  removeItem,
  updateItem,
} from '@ngxs/store/operators';
import { ITable } from '../../domain/table';
import { Navigate } from '@ngxs/router-plugin';

const defaults: TableStateModel = {
  data: [],
  errorMessage: '',
  loading: false,
  editTableForm: {
    model: undefined,
    dirty: false,
    status: '',
    errors: {},
  },
  addTableForm: {
    model: undefined,
    dirty: false,
    status: '',
    errors: {},
  },
};

@State<TableStateModel>({
  name: 'tables',
  defaults,
})
@Injectable()
export class TableState {
  @Selector() static errorMessage(state: TableStateModel) {
    return state.errorMessage;
  }

  @Selector() static loading(state: TableStateModel) {
    return state.loading;
  }

  @Selector() static tables(state: TableStateModel) {
    return state.data;
  }

  @Selector() static tablesList(state: TableStateModel) {
    return state.data.map(table => {
      return { ...table };
    });
  }

  @Selector() static table(state: TableStateModel) {
    return (id: string) => state.data.find(e => e.id === id);
  }

  constructor(private readonly expertService: TableService) {}

  @Action(ResetErrorMessage)
  resetErrorMessage(
    { patchState }: StateContext<TableStateModel>,
    {}: ResetErrorMessage
  ) {
    patchState({
      errorMessage: '',
    });
  }

  @Action(LoadTables)
  loadTables(
    { patchState, dispatch }: StateContext<TableStateModel>,
    {}: LoadTables
  ) {
    patchState({ loading: true });
    return this.expertService.getTables().pipe(
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

  @Action(AddTable)
  addTable(context: StateContext<TableStateModel>, {}: AddTable) {
    context.patchState({ loading: true });
    let formValues = context.getState().addTableForm.model;
    return this.expertService.createTable(formValues).pipe(
      tap(res => {
        context.setState(
          patch<TableStateModel>({
            data: insertItem<ITable>(res.data),
          })
        );
        context.dispatch([
          new ResetForm({ path: 'tables.addTableForm' }),
          new Navigate(['/tables']),
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

  @Action(UpdateTable)
  updateTable(context: StateContext<TableStateModel>, action: UpdateTable) {
    context.patchState({ loading: true });
    let formValues = context.getState().editTableForm.model;
    return this.expertService.updateTable(action.tableId, formValues).pipe(
      tap(res => {
        context.setState(
          patch<TableStateModel>({
            data: updateItem<ITable>(
              table => table.id === action.tableId,
              res.data
            ),
          })
        );
        context.dispatch([
          new ResetForm({ path: 'tables.editTableForm' }),
          new Navigate(['/tables']),
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

  @Action(DeleteTable)
  deleteTable(context: StateContext<TableStateModel>, action: DeleteTable) {
    context.patchState({ loading: true });
    return this.expertService.deleteTable(action.tableId).pipe(
      tap(_ => {
        context.setState(
          patch<TableStateModel>({
            data: removeItem<ITable>(table => table.id === action.tableId),
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
