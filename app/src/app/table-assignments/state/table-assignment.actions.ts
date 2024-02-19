import { IReservationAssignmentModel } from '../../models/reservation-assignment-model';

export class LoadReservations {
  static readonly type = '[Table Assignment] Load reservations';
  constructor() {}
}

export class LoadTables {
  static readonly type = '[Table Assignment] Load tables';
  constructor(public areaSlotId: string) {}
}

export class ResetErrorMessage {
  static readonly type = '[Table Assignment] Reset error message';
  constructor() {}
}

export class SetSelectedDate {
  static readonly type = '[Table Assignment] Set selected date';
  constructor(public selectedDate: Date) {}
}

export class SetSelectedArea {
  static readonly type = '[Table Assignment] Set selected area';
  constructor(public areaId: string) {}
}

export class SetSelectedAreaSlot {
  static readonly type = '[Table Assignment] Set selected area slot';
  constructor(public areaSlotId: string) {}
}

export class AddReservationAssignment {
  static readonly type = '[Reservation] Add reservation assignment';
  constructor(public model: IReservationAssignmentModel) {}
}

export class ClearState {
  static readonly type = '[Table Assignment] Clear state';
  constructor() {}
}
