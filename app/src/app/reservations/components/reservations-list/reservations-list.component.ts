import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ReservationFacade } from '../../../reservations-core/state/reservation.facade';

@Component({
  selector: 'reservations-list',
  templateUrl: './reservations-list.component.html',
  styleUrls: ['./reservations-list.component.scss'],
})
export class ReservationsListComponent implements OnInit {
  cols = [
    { title: 'ID', field: 'id' },
    { title: 'Name', field: 'name' },
    { title: 'Adresse', field: 'address' },
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
      onClick: (id: string) => this.reservationFacade.deleteReservation(id),
    },
  ];

  constructor(
    readonly reservationFacade: ReservationFacade,
    private readonly router: Router
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
}
