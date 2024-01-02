import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ReservationFacade } from '../../../reservations-core/state/reservation.facade';
import { ConfirmationService } from 'primeng/api';
import { combineLatestWith, map, Observable } from 'rxjs';
import { IReservation } from '../../../domain/reservation';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'reservations-list',
  templateUrl: './reservations-list.component.html',
  styleUrls: ['./reservations-list.component.scss'],
  providers: [ConfirmationService],
})
export class ReservationsListComponent implements OnInit, OnDestroy {
  cols = [
    { title: 'Zeitpunkt', field: 'bookingDateString' },
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
  minDate = new Date();
  data$: Observable<IReservation[]>;

  constructor(
    readonly reservationFacade: ReservationFacade,
    private readonly router: Router,
    private readonly confirmationService: ConfirmationService,
    private readonly datePipe: DatePipe
  ) {}

  ngOnInit(): void {
    this.reservationFacade.loadReservations();
    this.data$ = this.reservationFacade.selectedDate.pipe(
      combineLatestWith(this.reservationFacade.reservationsList),
      map(([selectedDate, reservations]) => {
        if (selectedDate) {
          return reservations.filter(
            reservation =>
              new Date(reservation.bookingDate).setHours(0, 0, 0, 0) ===
              selectedDate.setHours(0, 0, 0, 0)
          );
        }
        return reservations;
      }),
      map(reservations =>
        reservations.map(reservation => ({
          ...reservation,
          bookingDateString: this.datePipe.transform(
            new Date(reservation.bookingDate),
            'short',
            'UTC'
          ),
        }))
      )
    );
  }

  navigateToAddForm() {
    this.router.navigate(['reservations/add']);
  }

  navigateToEditForm(id: string) {
    this.router.navigate(['reservations/' + id + '/edit']);
  }

  onDateSelect(date: Date): void {
    this.reservationFacade.setSelectedDate(date);
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
      reject: () => {
        this.confirmationService.close();
      },
    });
  }

  ngOnDestroy(): void {
    this.reservationFacade.setSelectedDate(null);
  }
}
