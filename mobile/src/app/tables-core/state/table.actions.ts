export class LoadTables {
  static readonly type = '[Table] Load tables';
  constructor() {}
}

export class ResetErrorMessage {
  static readonly type = '[Table] Reset error message';
  constructor() {}
}

export class AddTable {
  static readonly type = '[Table] Add table';
  constructor() {}
}

export class UpdateTable {
  static readonly type = '[Table] Update table';
  constructor(public tableId: string) {}
}

export class DeleteTable {
  static readonly type = '[Table] Delete table';
  constructor(public tableId: string) {}
}
