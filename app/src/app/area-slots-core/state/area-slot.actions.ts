export class LoadAreaSlots {
  static readonly type = '[AreaSlot] Load area slots';
  constructor() {}
}

export class ResetErrorMessage {
  static readonly type = '[AreaSlot] Reset error message';
  constructor() {}
}

export class AddAreaSlot {
  static readonly type = '[AreaSlot] Add area slot';
  constructor() {}
}

export class UpdateAreaSlot {
  static readonly type = '[AreaSlot] Update area slot';
  constructor(public areaSlotId: string) {}
}

export class DeleteAreaSlot {
  static readonly type = '[AreaSlot] Delete area slot';
  constructor(public areaSlotId: string) {}
}
