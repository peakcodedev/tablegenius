import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ReservationFacade } from '../../../reservations-core/state/reservation.facade';
import { ConfirmationService } from 'primeng/api';

@Component({
  selector: 'reservations-list',
  templateUrl: './reservations-list.component.html',
  styleUrls: ['./reservations-list.component.scss'],
  providers: [ConfirmationService],
})
export class ReservationsListComponent implements OnInit {
  cols = [
    { title: 'Zeitpunkt', field: 'bookingDate' },
    { title: 'Name', field: 'name' },
    { title: 'Anzahl', field: 'count' },
  ];
  tableActions = [
    {
      label: 'Bearbeiten',
      icon: 'pencil',
      style: 'text',
      onClick: (id: string) => this.navigateToEditForm(id),
    },
    {
      label: 'Löschen',
      icon: 'trash',
      style: 'text',
      onClick: (id: string) => this.displayDeleteConfirmDialog(id),
    },
  ];

  constructor(
    readonly reservationFacade: ReservationFacade,
    private readonly router: Router,
    private readonly confirmationService: ConfirmationService
  ) {}

  ngOnInit(): void {
    this.reservationFacade.loadReservations();
  }

  navigateToAddForm() {
    this.router.navigate(['reservations/add']);
  }

  navigateToEditForm(id: string) {
    this.router.navigate(['reservations/' + id + '/edit']);
  }

  private displayDeleteConfirmDialog(id: string) {
    this.confirmationService.confirm({
      message:
        'Bist du dir sicher, dass du diese Reservation löschen möchtest?',
      header: 'Reservation löschen',
      acceptLabel: 'Ja',
      rejectLabel: 'Nein',
      icon: 'pi pi-info-circle',
      accept: () => {
        this.reservationFacade.deleteReservation(id);
      },
      reject: () => {},
    });
  }
}
