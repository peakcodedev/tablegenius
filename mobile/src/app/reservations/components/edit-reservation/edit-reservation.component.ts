import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { take, tap } from 'rxjs';
import { ReservationFacade } from '../../../reservations-core/state/reservation.facade';

@Component({
  selector: 'edit-reservation',
  templateUrl: './edit-reservation.component.html',
  styleUrls: ['./edit-reservation.component.scss'],
})
export class EditReservationComponent implements OnInit {
  form: FormGroup;
  reservationId: string;

  constructor(
    readonly reservationFacade: ReservationFacade,
    private readonly router: Router,
    private readonly route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.reservationId = this.route.snapshot.paramMap.get('reservationId');
    this.reservationFacade
      .byId$(this.reservationId)
      .pipe(
        take(1),
        tap(reservation => {
          if (!reservation || reservation.id !== this.reservationId) {
            this.navigateToList();
          }
          const bookingDate = reservation?.bookingDate.substring(
            0,
            reservation?.bookingDate.lastIndexOf(':')
          );
          this.form = new FormGroup({
            name: new FormControl(reservation.name, Validators.required),
            bookingDate: new FormControl(bookingDate, []),
            comments: new FormControl(reservation.comments, []),
            count: new FormControl(reservation.count, Validators.required),
            phoneNumber: new FormControl(
              reservation.phoneNumber,
              Validators.required
            ),
          });
        })
      )
      .subscribe();
  }

  navigateToList() {
    this.router.navigate(['tabs/reservations']);
  }
}
