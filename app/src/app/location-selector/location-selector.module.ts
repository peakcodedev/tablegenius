import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgxsModule } from '@ngxs/store';
import { CanActivateReservations } from '../reservations-core/guards/can-activate-reservations.service';
import { LocationSelectionFacade } from './state/location-selection.facade';
import { UserLocationsService } from './services/user-locations.service';
import { LocationSelectionState } from './state/location-selection.state';
import { locationSelectionRouting } from './location-selection-routing.module';
import { LocationSelectorComponent } from './components/location-selector/location-selector.component';

@NgModule({
  declarations: [LocationSelectorComponent],
  imports: [
    CommonModule,
    NgxsModule.forFeature([LocationSelectionState]),
    locationSelectionRouting,
  ],
  providers: [
    LocationSelectionFacade,
    UserLocationsService,
    CanActivateReservations,
  ],
})
export class LocationSelectorModule {}
