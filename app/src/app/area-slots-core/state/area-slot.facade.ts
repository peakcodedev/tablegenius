import { Injectable } from '@angular/core';
import { Dispatch } from '@ngxs-labs/dispatch-decorator';
import { Select } from '@ngxs/store';
import { map, Observable } from 'rxjs';
import { AreaSlotState } from './area-slot.state';
import {
  AddAreaSlot,
  DeleteAreaSlot,
  LoadAreaSlots,
  UpdateAreaSlot,
} from './area-slot.actions';
import { AppState } from '../../core/app-state';
import { IAreaSlot } from '../../domain/area-slot';

@Injectable()
export class AreaSlotFacade {
  @Select(AreaSlotState.areaSlots)
  areaSlots: Observable<IAreaSlot[]>;
  @Select(AreaSlotState.areaSlotsList)
  areaSlotsList: Observable<IAreaSlot[]>;
  @Select(AreaSlotState.loading)
  loading: Observable<boolean>;
  @Select(AreaSlotState.errorMessage)
  errorMessage: Observable<string>;

  constructor(private readonly appState: AppState) {}

  byId$(id: string) {
    return this.appState
      .select$(AreaSlotState.areaSlot)
      .pipe(map(selector => selector(id)));
  }

  @Dispatch()
  loadAreaSlots = () => new LoadAreaSlots();
  loadAreaSlots$ = () => this.appState.dispatch(new LoadAreaSlots());

  @Dispatch()
  addAreaSlot = () => new AddAreaSlot();

  @Dispatch()
  deleteAreaSlot = (areaSlotId: string) => new DeleteAreaSlot(areaSlotId);

  @Dispatch()
  updateAreaSlot = (areaSlotId: string) => new UpdateAreaSlot(areaSlotId);
}
