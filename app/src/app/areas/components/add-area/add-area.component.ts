import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { Router } from '@angular/router';
import { AreaFacade } from '../../../areas-core/state/area.facade';

@Component({
  selector: 'add-area',
  templateUrl: './add-area.component.html',
  styleUrls: ['./add-area.component.scss'],
})
export class AddAreaComponent implements OnInit {
  form: FormGroup;

  constructor(
    readonly areaFacade: AreaFacade,
    private readonly formBuilder: FormBuilder,
    private readonly router: Router
  ) {}

  ngOnInit(): void {
    this.form = new FormGroup({
      name: new FormControl('', Validators.required),
      blueprintUrl: new FormControl('', []),
    });
  }

  navigateToList() {
    this.router.navigate(['areas']);
  }
}
