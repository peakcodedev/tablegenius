import { Component, Input } from '@angular/core';
import { IReservation } from '../../../domain/reservation';

@Component({
  selector: 'app-reservation-assignment-detail',
  templateUrl: './reservation-assignment-detail.component.html',
  styleUrls: ['./reservation-assignment-detail.component.scss'],
})
export class ReservationAssignmentDetailComponent {
  @Input() reservation: IReservation;
}
