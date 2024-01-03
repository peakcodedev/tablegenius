import { Injectable } from '@angular/core';
import { Dispatch } from '@ngxs-labs/dispatch-decorator';
import { Select } from '@ngxs/store';
import { map, Observable } from 'rxjs';
import { AreaState } from './area.state';
import { AddArea, DeleteArea, LoadAreas, UpdateArea } from './area.actions';
import { AppState } from '../../core/app-state';
import { IArea } from '../../domain/area';

@Injectable()
export class AreaFacade {
  @Select(AreaState.areas)
  areas: Observable<IArea[]>;
  @Select(AreaState.areasList)
  areasList: Observable<IArea[]>;
  @Select(AreaState.loading)
  loading: Observable<boolean>;
  @Select(AreaState.errorMessage)
  errorMessage: Observable<string>;

  constructor(private readonly appState: AppState) {}

  byId$(id: string) {
    return this.appState
      .select$(AreaState.area)
      .pipe(map(selector => selector(id)));
  }

  @Dispatch()
  loadAreas = () => new LoadAreas();
  loadAreas$ = () => this.appState.dispatch(new LoadAreas());

  @Dispatch()
  addArea = () => new AddArea();

  @Dispatch()
  deleteArea = (areaId: string) => new DeleteArea(areaId);

  @Dispatch()
  updateArea = (areaId: string) => new UpdateArea(areaId);
}
