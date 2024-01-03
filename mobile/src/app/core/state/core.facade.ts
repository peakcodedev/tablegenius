import { Injectable } from '@angular/core';
import { Dispatch } from '@ngxs-labs/dispatch-decorator';
import { Select } from '@ngxs/store';
import { Observable } from 'rxjs';
import { CoreState } from './core.state';
import { SetTenantId } from './core.actions';

@Injectable()
export class CoreFacade {
  @Select(CoreState.tenantId)
  tenantId: Observable<string>;

  @Dispatch()
  setTenantId = (tenantId: string) => new SetTenantId(tenantId);
}
