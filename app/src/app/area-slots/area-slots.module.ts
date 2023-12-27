import { NgModule } from '@angular/core';
import { AsyncPipe, CommonModule } from '@angular/common';
import { AreaSlotsListComponent } from './components/area-slots-list/area-slots-list.component';
import { AddAreaSlotComponent } from './components/add-area-slot/add-area-slot.component';
import { DesignModule } from '../design/design.module';
import { InputTextModule } from 'primeng/inputtext';
import { ReactiveFormsModule } from '@angular/forms';
import { NgxsFormPluginModule } from '@ngxs/form-plugin';
import { EditAreaSlotComponent } from './components/edit-area-slot/edit-area-slot.component';
import { CoreModule } from '../core/core.module';
import { DropdownModule } from 'primeng/dropdown';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { AreasCoreModule } from '../areas-core/areas-core.module';
import { areaSlotsRouting } from './area-slots-routing.module';
import { AreaSlotsCoreModule } from '../area-slots-core/area-slots-core.module';

@NgModule({
  declarations: [
    AreaSlotsListComponent,
    AddAreaSlotComponent,
    EditAreaSlotComponent,
  ],
  imports: [
    CommonModule,
    areaSlotsRouting,
    DesignModule,
    InputTextModule,
    ReactiveFormsModule,
    NgxsFormPluginModule,
    CoreModule,
    AreaSlotsCoreModule,
    AreasCoreModule,
    DropdownModule,
    InputTextareaModule,
  ],
  providers: [AsyncPipe],
})
export class AreaSlotsModule {}
