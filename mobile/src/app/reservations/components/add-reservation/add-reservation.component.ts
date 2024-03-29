import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ReservationFacade } from '../../../reservations-core/state/reservation.facade';

@Component({
  selector: 'add-reservation',
  templateUrl: './add-reservation.component.html',
  styleUrls: ['./add-reservation.component.scss'],
})
export class AddReservationComponent implements OnInit {
  form: FormGroup;

  constructor(
    readonly reservationFacade: ReservationFacade,
    private readonly router: Router
  ) {}

  ngOnInit(): void {
    this.form = new FormGroup({
      name: new FormControl('', Validators.required),
      bookingDate: new FormControl(new Date(), []),
      comments: new FormControl('', []),
      count: new FormControl('', Validators.required),
      phoneNumber: new FormControl('', Validators.required),
    });
  }

  navigateToList() {
    this.router.navigate(['tabs/reservations']);
  }
}
