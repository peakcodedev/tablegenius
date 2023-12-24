export class SetTenantId {
  static readonly type = '[Core] Set tenant id';
  constructor(public tenantId: string) {}
}
