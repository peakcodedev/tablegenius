import { IReservationAssignment } from '../../domain/reservation-assignment';
import { ITable } from '../../domain/table';

export class ReservationAssignmentStateModel {
  public selectedDate: Date;
  public selectedArea: string;
  public selectedAreaSlot: string;
  public assignments: IReservationAssignment[];
  public freeTables: ITable[];
  public loading: boolean;
}
