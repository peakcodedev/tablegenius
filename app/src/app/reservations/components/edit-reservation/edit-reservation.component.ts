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
          this.form = new FormGroup({
            name: new FormControl(location.name, Validators.required),
            logoUrl: new FormControl(location.logoUrl, []),
            description: new FormControl(location.description, []),
            address: new FormControl(location.address, Validators.required),
          });
        })
      )
      .subscribe();
  }

  navigateToList() {
    this.router.navigate(['reservations']);
  }
}
