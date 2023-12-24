import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { locationsRouting } from './locations-routing.module';
import { LocationsListComponent } from './components/locations-list/locations-list.component';
import { AddLocationComponent } from './components/add-location/add-location.component';
import { DesignModule } from '../design/design.module';
import { InputTextModule } from 'primeng/inputtext';
import { ReactiveFormsModule } from '@angular/forms';
import { NgxsFormPluginModule } from '@ngxs/form-plugin';
import { EditLocationComponent } from './components/edit-location/edit-location.component';
import { CoreModule } from '../core/core.module';
import { DropdownModule } from 'primeng/dropdown';
import { LocationCoreModule } from '../locations-core/location-core.module';
import { InputTextareaModule } from 'primeng/inputtextarea';

@NgModule({
  declarations: [
    LocationsListComponent,
    AddLocationComponent,
    EditLocationComponent,
  ],
  imports: [
    CommonModule,
    locationsRouting,
    DesignModule,
    InputTextModule,
    ReactiveFormsModule,
    NgxsFormPluginModule,
    CoreModule,
    LocationCoreModule,
    DropdownModule,
    InputTextareaModule,
  ],
  providers: [],
})
export class LocationsModule {}
