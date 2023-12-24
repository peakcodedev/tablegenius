import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgxsFormPluginModule } from '@ngxs/form-plugin';
import { NgxsModule } from '@ngxs/store';
import { CoreModule } from '../core/core.module';
import { TableState } from './state/table.state';
import { TableFacade } from './state/table.facade';
import { TableService } from './services/table.service';
import { CanActivateTables } from './guards/can-activate-tables.service';

@NgModule({
  declarations: [],
  imports: [
    NgxsModule.forFeature([TableState]),
    CommonModule,
    NgxsFormPluginModule,
    CoreModule,
  ],
  providers: [TableFacade, TableService, CanActivateTables],
  exports: [],
})
export class TablesCoreModule {}
