import { Injectable } from '@angular/core';
import { Action, Selector, State, StateContext } from '@ngxs/store';
import { SetTenantId } from './core.actions';
import { CoreStateModel } from './core-state-model';

const defaults: CoreStateModel = {
  tenantId: '',
};

@State<CoreStateModel>({
  name: 'core',
  defaults,
})
@Injectable()
export class CoreState {
  @Selector() static tenantId(state: CoreStateModel) {
    return state.tenantId;
  }

  @Action(SetTenantId)
  setTenantId(
    { patchState }: StateContext<CoreStateModel>,
    { tenantId }: SetTenantId
  ) {
    patchState({
      tenantId,
    });
  }
}
