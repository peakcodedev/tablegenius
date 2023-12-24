import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { take, tap } from 'rxjs';
import { LocationFacade } from '../../../locations-core/state/location.facade';

@Component({
  selector: 'edit-location',
  templateUrl: './edit-location.component.html',
  styleUrls: ['./edit-location.component.scss'],
})
export class EditLocationComponent implements OnInit {
  form: FormGroup;
  locationId: string;

  constructor(
    readonly locationFacade: LocationFacade,
    private readonly formBuilder: FormBuilder,
    private readonly router: Router,
    private readonly route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.locationId = this.route.snapshot.paramMap.get('locationId');
    this.locationFacade
      .byId$(this.locationId)
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
    this.router.navigate(['locations']);
  }
}
