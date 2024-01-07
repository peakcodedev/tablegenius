import { ILocation } from '../../domain/location';
import { ILocationAssignment } from '../../domain/location-assignment';

export class LocationStateModel {
  public data: ILocation[];
  public assignments: ILocationAssignment[];
  public loading: boolean;
  public errorMessage: string;
  public editLocationForm: any;
  public addLocationForm: any;
  public addLocationAssignmentForm: any;
}
