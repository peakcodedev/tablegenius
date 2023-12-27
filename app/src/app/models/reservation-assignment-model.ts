import { ITableReservationAssignmentModel } from './table-reservation-assignment-model';

export interface IReservationAssignmentModel {
  bookingDate: Date;
  areaSlotId: string;
  reservationId: string;
  tableReservationAssignments: ITableReservationAssignmentModel[];
}
