import { ITable } from '../../domain/table';

export class TableStateModel {
  public data: ITable[];
  public loading: boolean;
  public errorMessage: string;
  public editTableForm: any;
  public addTableForm: any;
}
