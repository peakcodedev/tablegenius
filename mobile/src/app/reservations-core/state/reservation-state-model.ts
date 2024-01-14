import { IReservation } from '../../domain/reservation';

export class ReservationStateModel {
  public data: IReservation[];
  public loading: boolean;
  public searchString: string;
  public selectedDate: Date;
  public errorMessage: string;
  public editReservationForm: any;
  public addReservationForm: any;
}
