import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
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
    private readonly formBuilder: FormBuilder,
    private readonly router: Router,
    private readonly route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.reservationId = this.route.snapshot.paramMap.get('reservationId');
    this.reservationFacade
      .byId$(this.reservationId)
      .pipe(
        take(1),
        tap(location => {
          if (!location) {
            this.navigateToList();
          }
          const bookingDate = location?.bookingDate.substring(
            0,
            location?.bookingDate.lastIndexOf(':')
          );
          this.form = new FormGroup({
            name: new FormControl(location.name, Validators.required),
            bookingDate: new FormControl(bookingDate, []),
            comments: new FormControl(location.comments, []),
            count: new FormControl(location.count, Validators.required),
            phoneNumber: new FormControl(
              location.phoneNumber,
              Validators.required
            ),
          });
        })
      )
      .subscribe();
  }

  navigateToList() {
    this.router.navigate(['reservations']);
  }
}
