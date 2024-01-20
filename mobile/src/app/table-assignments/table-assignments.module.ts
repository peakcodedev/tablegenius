import { NgModule } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';

import { tableAssignmentsRouting } from './table-assignments-routing.module';
import { TableAssignmentsOverviewComponent } from './components/table-assignments-overview/table-assignments-overview.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgxsFormPluginModule } from '@ngxs/form-plugin';
import { CoreModule } from '../core/core.module';
import { ReservationCoreModule } from '../reservations-core/reservation-core.module';
import { NgxsModule } from '@ngxs/store';
import { TableAssignmentFacade } from './state/table-assignment.facade';
import { AreasCoreModule } from '../areas-core/areas-core.module';
import { TableAssignmentState } from './state/table-assignment.state';
import { TablesCoreModule } from '../tables-core/tables-core.module';
import { AreaSlotsCoreModule } from '../area-slots-core/area-slots-core.module';
import { IonicModule } from '@ionic/angular';
import { ReservationSelectionModalComponent } from './modals/reservation-selection/reservation-selection-modal.component';
import { TableSelectionModalComponent } from './modals/table-selection/table-selection-modal.component';

@NgModule({
  declarations: [
    TableAssignmentsOverviewComponent,
    ReservationSelectionModalComponent,
    TableSelectionModalComponent,
  ],
  imports: [
    NgxsModule.forFeature([TableAssignmentState]),
    CommonModule,
    tableAssignmentsRouting,
    FormsModule,
    ReactiveFormsModule,
    NgxsFormPluginModule,
    CoreModule,
    ReservationCoreModule,
    TablesCoreModule,
    AreasCoreModule,
    AreaSlotsCoreModule,
    IonicModule,
  ],
  providers: [DatePipe, TableAssignmentFacade],
})
export class TableAssignmentsModule {}
