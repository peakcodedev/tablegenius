import { IAreaSlot } from '../../domain/area-slot';

export class AreaSlotStateModel {
  public data: IAreaSlot[];
  public loading: boolean;
  public errorMessage: string;
  public editAreaSlotForm: any;
  public addAreaSlotForm: any;
}
