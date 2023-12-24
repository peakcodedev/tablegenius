import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { Router } from '@angular/router';
import { LocationFacade } from '../../../locations-core/state/location.facade';

@Component({
  selector: 'add-location',
  templateUrl: './add-location.component.html',
  styleUrls: ['./add-location.component.scss'],
})
export class AddLocationComponent implements OnInit {
  form: FormGroup;

  constructor(
    readonly locationFacade: LocationFacade,
    private readonly formBuilder: FormBuilder,
    private readonly router: Router
  ) {}

  ngOnInit(): void {
    this.form = new FormGroup({
      name: new FormControl('', Validators.required),
      logoUrl: new FormControl('', []),
      description: new FormControl('', []),
      address: new FormControl('', Validators.required),
    });
  }

  navigateToList() {
    this.router.navigate(['locations']);
  }
}
