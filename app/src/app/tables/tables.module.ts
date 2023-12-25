import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { tablesRouting } from './tables-routing.module';
import { TablesListComponent } from './components/tables-list/tables-list.component';
import { AddTableComponent } from './components/add-table/add-table.component';
import { DesignModule } from '../design/design.module';
import { InputTextModule } from 'primeng/inputtext';
import { ReactiveFormsModule } from '@angular/forms';
import { NgxsFormPluginModule } from '@ngxs/form-plugin';
import { EditTableComponent } from './components/edit-table/edit-table.component';
import { CoreModule } from '../core/core.module';
import { TablesCoreModule } from '../tables-core/tables-core.module';
import { DropdownModule } from 'primeng/dropdown';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { AreasCoreModule } from '../areas-core/areas-core.module';

@NgModule({
  declarations: [TablesListComponent, AddTableComponent, EditTableComponent],
  imports: [
    CommonModule,
    tablesRouting,
    DesignModule,
    InputTextModule,
    ReactiveFormsModule,
    NgxsFormPluginModule,
    CoreModule,
    TablesCoreModule,
    AreasCoreModule,
    DropdownModule,
    InputTextareaModule,
  ],
  providers: [],
})
export class TablesModule {}
