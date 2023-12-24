import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { TableFacade } from '../../../tables-core/state/table.facade';
import { Router } from '@angular/router';

@Component({
  selector: 'add-table',
  templateUrl: './add-table.component.html',
  styleUrls: ['./add-table.component.scss'],
})
export class AddTableComponent implements OnInit {
  form: FormGroup;

  constructor(
    readonly tableFacade: TableFacade,
    private readonly formBuilder: FormBuilder,
    private readonly router: Router
  ) {}

  ngOnInit(): void {
    this.form = new FormGroup({
      tableNumber: new FormControl('', Validators.required),
      capacity: new FormControl('', Validators.required),
      description: new FormControl('', []),
    });
  }

  navigateToList() {
    this.router.navigate(['tables']);
  }
}
