import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgxsFormPluginModule } from '@ngxs/form-plugin';
import { NgxsModule } from '@ngxs/store';
import { CoreModule } from '../core/core.module';
import { LocationState } from './state/location.state';
import { LocationFacade } from './state/location.facade';
import { LocationService } from './services/location.service';
import { CanActivateLocations } from './guards/can-activate-locations.service';

@NgModule({
  declarations: [],
  imports: [
    NgxsModule.forFeature([LocationState]),
    CommonModule,
    NgxsFormPluginModule,
    CoreModule,
  ],
  providers: [LocationFacade, LocationService, CanActivateLocations],
  exports: [],
})
export class LocationCoreModule {}
