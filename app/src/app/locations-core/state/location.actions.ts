export class LoadLocations {
  static readonly type = '[Location] Load locations';
  constructor() {}
}

export class LoadLocationAssignments {
  static readonly type = '[Location] Load location assignments';
  constructor(public locationId: string) {}
}

export class ResetErrorMessage {
  static readonly type = '[Location] Reset error message';
  constructor() {}
}

export class AddLocation {
  static readonly type = '[Location] Add location';
  constructor() {}
}

export class UpdateLocation {
  static readonly type = '[Location] Update location';
  constructor(public locationId: string) {}
}

export class DeleteLocation {
  static readonly type = '[Location] Delete location';
  constructor(public locationId: string) {}
}
