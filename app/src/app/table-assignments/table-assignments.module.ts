import { NgModule } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';

import { tableAssignmentsRouting } from './table-assignments-routing.module';
import { TableAssignmentsOverviewComponent } from './components/table-assignments-overview/table-assignments-overview.component';
import { DesignModule } from '../design/design.module';
import { InputTextModule } from 'primeng/inputtext';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgxsFormPluginModule } from '@ngxs/form-plugin';
import { CoreModule } from '../core/core.module';
import { DropdownModule } from 'primeng/dropdown';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { ReservationCoreModule } from '../reservations-core/reservation-core.module';
import { CalendarModule } from 'primeng/calendar';
import { NgxsModule } from '@ngxs/store';
import { TableAssignmentFacade } from './state/table-assignment.facade';
import { AreasCoreModule } from '../areas-core/areas-core.module';
import { TableAssignmentState } from './state/table-assignment.state';
import { TablesCoreModule } from '../tables-core/tables-core.module';
import { DragDropModule } from 'primeng/dragdrop';
import { AreaSlotsCoreModule } from '../area-slots-core/area-slots-core.module';

@NgModule({
  declarations: [TableAssignmentsOverviewComponent],
  imports: [
    NgxsModule.forFeature([TableAssignmentState]),
    CommonModule,
    tableAssignmentsRouting,
    DesignModule,
    InputTextModule,
    FormsModule,
    ReactiveFormsModule,
    NgxsFormPluginModule,
    CoreModule,
    ReservationCoreModule,
    DropdownModule,
    InputTextareaModule,
    TablesCoreModule,
    AreasCoreModule,
    AreaSlotsCoreModule,
    CalendarModule,
    DragDropModule,
  ],
  providers: [DatePipe, TableAssignmentFacade],
})
export class TableAssignmentsModule {}
