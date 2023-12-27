export class LoadLocationsOfUser {
  static readonly type = '[Location Selection] Load locations of user';
  constructor() {}
}

export class ResetErrorMessage {
  static readonly type = '[Location Selection] Reset error message';
  constructor() {}
}

export class SetLocation {
  static readonly type = '[Location Selection] Set location';
  constructor(public locationId: string) {}
}
