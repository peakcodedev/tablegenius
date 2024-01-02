import { IAreaSlot } from './area-slot';
import { IReservation } from './reservation';
import { ITable } from './table';

export interface IReservationAssignment {
  id: string;
  bookingDate: string;
  areaSlot: IAreaSlot;
  reservation: IReservation;
  tableReservationAssignments: ITableReservationAssignment[];
}

export interface ITableReservationAssignment {
  reservationAssignmentId: string;
  table: ITable;
}
