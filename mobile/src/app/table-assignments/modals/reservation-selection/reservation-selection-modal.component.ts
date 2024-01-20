import { Component, Input } from '@angular/core';

import { ModalController } from '@ionic/angular';
import { IReservation } from '../../../domain/reservation';

@Component({
  selector: 'reservation-selection-modal',
  templateUrl: 'reservation-selection-modal.component.html',
  styleUrls: ['reservation-selection-modal.component.scss'],
})
export class ReservationSelectionModalComponent {
  @Input() reservations: IReservation[];

  constructor(private modalCtrl: ModalController) {}

  cancel() {
    return this.modalCtrl.dismiss(null, 'cancel');
  }

  onReservationSelected(reservation: IReservation) {
    return this.modalCtrl.dismiss(reservation, 'confirm');
  }
}
