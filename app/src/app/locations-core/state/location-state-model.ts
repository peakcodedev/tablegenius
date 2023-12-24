import { ILocation } from '../../domain/location';

export class LocationStateModel {
  public data: ILocation[];
  public loading: boolean;
  public errorMessage: string;
  public editLocationForm: any;
  public addLocationForm: any;
}
