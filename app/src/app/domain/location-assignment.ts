import { ILocation } from './location';

export interface ILocationAssignment {
  id: string;
  mail: string;
  locationId: string;
  location: ILocation;
}
