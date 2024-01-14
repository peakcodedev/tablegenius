export class LoadReservations {
  static readonly type = '[Reservation] Load reservations';
  constructor() {}
}

export class ResetErrorMessage {
  static readonly type = '[Reservation] Reset error message';
  constructor() {}
}

export class SetSelectedDate {
  static readonly type = '[Reservation] Set selected date';
  constructor(public selectedDate: Date) {}
}

export class SetSearchString {
  static readonly type = '[Reservation] Set search string';
  constructor(public searchString: string) {}
}

export class AddReservation {
  static readonly type = '[Reservation] Add reservation';
  constructor() {}
}

export class UpdateReservation {
  static readonly type = '[Reservation] Update reservation';
  constructor(public reservationId: string) {}
}

export class DeleteReservation {
  static readonly type = '[Reservation] Delete reservation';
  constructor(public reservationId: string) {}
}
