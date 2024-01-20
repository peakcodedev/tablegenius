import { Component, Input, OnInit } from '@angular/core';
import { IReservation } from '../../../domain/reservation';
import { ITable } from '../../../domain/table';
import { ReservationFacade } from '../../../reservations-core/state/reservation.facade';
import { PopoverController } from '@ionic/angular';

@Component({
  selector: 'delete-reservation-confirmation',
  templateUrl: './delete-reservation-confirmation.component.html',
  styleUrls: ['./delete-reservation-confirmation.component.scss'],
})
export class DeleteReservationConfirmationComponent {
  @Input() reservation: IReservation;
  constructor(
    readonly reservationFacade: ReservationFacade,
    private readonly popoverController: PopoverController
  ) {}

  async delete(reservation: IReservation) {
    this.reservationFacade.deleteReservation(reservation.id);
    await this.closePopover();
  }

  async closePopover() {
    await this.popoverController.dismiss();
  }
}
