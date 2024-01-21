import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ReservationFacade } from '../../../reservations-core/state/reservation.facade';
import {
  combineLatest,
  interval,
  map,
  Observable,
  startWith,
  Subject,
  takeUntil,
  tap,
} from 'rxjs';
import { IReservation } from '../../../domain/reservation';
import { DatePipe } from '@angular/common';
import { AlertController } from '@ionic/angular';

@Component({
  selector: 'reservations-list',
  templateUrl: './reservations-list.component.html',
  styleUrls: ['./reservations-list.component.scss'],
})
export class ReservationsListComponent implements OnInit, OnDestroy {
  data$: Observable<IReservation[]>;
  destroy = new Subject<void>();
  minDate = new Date().toISOString();
  selectedDate: string = undefined;
  public alertButtons = [
    {
      text: 'Abbrechen',
      role: 'cancel',
    },
    {
      text: 'Löschen',
      role: 'confirm',
      handler: () => {
        this.reservationFacade.deleteReservation('');
      },
    },
  ];

  constructor(
    readonly reservationFacade: ReservationFacade,
    private readonly router: Router,
    private readonly datePipe: DatePipe,
    private readonly alertController: AlertController
  ) {}

  ngOnInit(): void {
    this.reservationFacade.loadReservations();
    this.data$ = combineLatest([
      this.reservationFacade.selectedDate.pipe(startWith(undefined)),
      this.reservationFacade.reservations.pipe(startWith([])),
      this.reservationFacade.searchString.pipe(startWith(undefined)),
    ]).pipe(
      takeUntil(this.destroy),
      map(([selectedDate, reservations, searchString]) => {
        if (selectedDate) {
          return reservations.filter(
            reservation =>
              new Date(reservation.bookingDate).setHours(0, 0, 0, 0) ===
              selectedDate.setHours(0, 0, 0, 0)
          );
        }
        if (searchString) {
          return reservations.filter(
            reservation =>
              reservation.name.includes(searchString) ||
              reservation.phoneNumber.includes(searchString) ||
              reservation.count == searchString
          );
        }
        return reservations;
      }),
      map(reservations =>
        reservations.map((reservation: IReservation) => ({
          ...reservation,
          bookingDateString: this.datePipe.transform(
            new Date(reservation.bookingDate),
            'short',
            'UTC'
          ),
        }))
      )
    );
    interval(1000 * 60 * 5)
      .pipe(takeUntil(this.destroy))
      .subscribe(_ => {
        this.refreshData();
      });
    this.reservationFacade.selectedDate
      .pipe(
        takeUntil(this.destroy),
        tap(value => {
          this.selectedDate = value?.toISOString();
        })
      )
      .subscribe();
  }

  refreshData() {
    this.reservationFacade.loadReservations();
  }

  setSearchString(event: CustomEvent): void {
    this.reservationFacade.setSearchString(event.detail.value);
  }

  navigateToAddForm() {
    this.router.navigate(['tabs/reservations/add']);
  }

  navigateToEditForm(id: string) {
    this.router.navigate(['tabs/reservations/' + id + '/edit']);
  }

  onDateSelect(event: CustomEvent): void {
    this.reservationFacade.setSelectedDate(new Date(event.detail.value));
  }

  clearSelectedDate() {
    this.reservationFacade.setSelectedDate(null);
  }

  ngOnDestroy(): void {
    this.reservationFacade.setSelectedDate(null);
    this.destroy.next();
    this.destroy.complete();
  }

  async openDeleteReservationConfirmationModal(reservation: IReservation) {
    const alert = await this.alertController.create({
      header: 'Reservation löschen',
      message:
        'Möchten Sie die Reservation "' +
        reservation.name +
        '" definitiv löschen?',
      buttons: [
        {
          text: 'NEIN',
          role: 'cancel',
        },
        {
          text: 'JA',
          role: 'confirm',
          handler: () => {
            this.reservationFacade.deleteReservation(reservation.id);
          },
        },
      ],
    });

    await alert.present();
  }
}
