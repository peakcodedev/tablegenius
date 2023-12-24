import { Injectable } from '@angular/core';
import { Dispatch } from '@ngxs-labs/dispatch-decorator';
import { Select } from '@ngxs/store';
import { map, Observable } from 'rxjs';
import { TableState } from './table.state';
import {
  AddTable,
  DeleteTable,
  LoadTables,
  UpdateTable,
} from './table.actions';
import { ITable } from '../../domain/table';
import { AppState } from '../../core/app-state';

@Injectable()
export class TableFacade {
  @Select(TableState.tables)
  experts: Observable<ITable[]>;
  @Select(TableState.tablesList)
  expertsList: Observable<ITable[]>;
  @Select(TableState.loading)
  loading: Observable<boolean>;
  @Select(TableState.errorMessage)
  errorMessage: Observable<string>;

  constructor(private readonly appState: AppState) {}

  byId$(id: string) {
    return this.appState
      .select$(TableState.table)
      .pipe(map(selector => selector(id)));
  }

  @Dispatch()
  loadTables = () => new LoadTables();
  loadTables$ = () => this.appState.dispatch(new LoadTables());

  @Dispatch()
  addTable = () => new AddTable();

  @Dispatch()
  deleteTable = (tableId: string) => new DeleteTable(tableId);

  @Dispatch()
  updateTable = (tableId: string) => new UpdateTable(tableId);
}
