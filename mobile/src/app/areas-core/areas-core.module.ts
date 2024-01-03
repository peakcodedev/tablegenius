import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgxsFormPluginModule } from '@ngxs/form-plugin';
import { NgxsModule } from '@ngxs/store';
import { CoreModule } from '../core/core.module';
import { AreaState } from './state/area.state';
import { AreaFacade } from './state/area.facade';
import { AreaService } from './services/area.service';
import { CanActivateAreas } from './guards/can-activate-areas.service';
import { AreasHelper } from './helpers/areas.helper';

@NgModule({
  declarations: [],
  imports: [
    NgxsModule.forFeature([AreaState]),
    CommonModule,
    NgxsFormPluginModule,
    CoreModule,
  ],
  providers: [AreaFacade, AreaService, CanActivateAreas, AreasHelper],
  exports: [],
})
export class AreasCoreModule {}
