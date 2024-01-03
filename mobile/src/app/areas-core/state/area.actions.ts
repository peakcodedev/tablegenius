export class LoadAreas {
  static readonly type = '[Area] Load areas';
  constructor() {}
}

export class ResetErrorMessage {
  static readonly type = '[Area] Reset error message';
  constructor() {}
}

export class AddArea {
  static readonly type = '[Area] Add area';
  constructor() {}
}

export class UpdateArea {
  static readonly type = '[Area] Update area';
  constructor(public areaId: string) {}
}

export class DeleteArea {
  static readonly type = '[Area] Delete area';
  constructor(public areaId: string) {}
}
