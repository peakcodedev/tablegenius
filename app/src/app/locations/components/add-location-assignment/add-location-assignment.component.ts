import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthenticationService } from '../../../core/services/authentication.service';
import { LocationFacade } from '../../../locations-core/state/location.facade';

@Component({
  selector: 'add-location-assignment',
  templateUrl: './add-location-assignment.component.html',
  styleUrls: ['./add-location-assignment.component.scss'],
})
export class AddLocationAssignmentComponent implements OnInit {
  form: FormGroup;
  locationId: string;
  constructor(
    readonly companyFacade: LocationFacade,
    private readonly formBuilder: FormBuilder,
    private readonly router: Router,
    private readonly authService: AuthenticationService,
    private readonly route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.form = this.formBuilder.group({
      mail: '',
      locationId: this.route.snapshot.paramMap.get('locationId'),
    });
  }

  navigateToList() {
    this.router.navigate(['locations']);
  }
}
