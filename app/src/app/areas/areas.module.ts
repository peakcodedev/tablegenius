import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { areasRouting } from './areas-routing.module';
import { AreasListComponent } from './components/areas-list/areas-list.component';
import { AddAreaComponent } from './components/add-area/add-area.component';
import { DesignModule } from '../design/design.module';
import { InputTextModule } from 'primeng/inputtext';
import { ReactiveFormsModule } from '@angular/forms';
import { NgxsFormPluginModule } from '@ngxs/form-plugin';
import { EditAreaComponent } from './components/edit-area/edit-area.component';
import { CoreModule } from '../core/core.module';
import { DropdownModule } from 'primeng/dropdown';
import { AreasCoreModule } from '../areas-core/areas-core.module';

@NgModule({
  declarations: [AreasListComponent, AddAreaComponent, EditAreaComponent],
  imports: [
    CommonModule,
    areasRouting,
    DesignModule,
    InputTextModule,
    ReactiveFormsModule,
    NgxsFormPluginModule,
    CoreModule,
    AreasCoreModule,
    DropdownModule,
  ],
  providers: [],
})
export class AreasModule {}
