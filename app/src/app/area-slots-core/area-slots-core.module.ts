import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgxsFormPluginModule } from '@ngxs/form-plugin';
import { NgxsModule } from '@ngxs/store';
import { CoreModule } from '../core/core.module';
import { AreaSlotState } from './state/area-slot.state';
import { AreaSlotFacade } from './state/area-slot.facade';
import { AreaSlotService } from './services/area-slot.service';
import { CanActivateAreaSlots } from './guards/can-activate-area-slots.service';

@NgModule({
  declarations: [],
  imports: [
    NgxsModule.forFeature([AreaSlotState]),
    CommonModule,
    NgxsFormPluginModule,
    CoreModule,
  ],
  providers: [AreaSlotFacade, AreaSlotService, CanActivateAreaSlots],
  exports: [],
})
export class AreaSlotsCoreModule {}
