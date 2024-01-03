import { ILocation } from '../../domain/location';

export class LocationSelectionStateModel {
  public locations: ILocation[];
  public loading: boolean;
  public errorMessage: string;
}
