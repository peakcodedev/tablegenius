import { IReservation } from '../../domain/reservation';
import { ITableWithStatus } from '../../domain/table';

export class TableAssignmentStateModel {
  public selectedDate: Date;
  public selectedArea: string;
  public selectedAreaSlot: string;
  public reservations: IReservation[];
  public assignments: any[];
  public tables: ITableWithStatus[];
}
