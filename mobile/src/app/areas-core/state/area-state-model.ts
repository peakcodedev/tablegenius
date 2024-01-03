import { IArea } from '../../domain/area';

export class AreaStateModel {
  public data: IArea[];
  public loading: boolean;
  public errorMessage: string;
  public editAreaForm: any;
  public addAreaForm: any;
}
