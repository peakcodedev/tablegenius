export class LoadAssignments {
  static readonly type = '[Reservation Assignment] Load assignments';
  constructor() {}
}

export class ResetErrorMessage {
  static readonly type = '[Reservation Assignment] Reset error message';
  constructor() {}
}

export class SetSelectedDate {
  static readonly type = '[Reservation Assignment] Set selected date';
  constructor(public selectedDate: Date) {}
}

export class SetSelectedArea {
  static readonly type = '[Reservation Assignment] Set selected area';
  constructor(public areaId: string) {}
}

export class SetSelectedAreaSlot {
  static readonly type = '[Reservation Assignment] Set selected area slot';
  constructor(public areaSlotId: string) {}
}
