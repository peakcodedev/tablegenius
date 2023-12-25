import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { TableFacade } from '../../../tables-core/state/table.facade';
import { Router } from '@angular/router';
import { AreasHelper } from '../../../areas-core/helpers/areas.helper';
import { Observable } from 'rxjs';

@Component({
  selector: 'add-table',
  templateUrl: './add-table.component.html',
  styleUrls: ['./add-table.component.scss'],
})
export class AddTableComponent implements OnInit {
  form: FormGroup;
  areas: Observable<any[]>;

  constructor(
    readonly tableFacade: TableFacade,
    private readonly formBuilder: FormBuilder,
    private readonly router: Router,
    readonly areasHelper: AreasHelper
  ) {}

  ngOnInit(): void {
    this.form = new FormGroup({
      tableNumber: new FormControl('', Validators.required),
      capacity: new FormControl('', Validators.required),
      description: new FormControl('', []),
      areaId: new FormControl(null, Validators.required),
    });
    this.areas = this.areasHelper.availableAreas();
  }

  navigateToList() {
    this.router.navigate(['tables']);
  }
}
