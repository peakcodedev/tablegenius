import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { TableFacade } from '../../../tables-core/state/table.facade';
import { ActivatedRoute, Router } from '@angular/router';
import { take, tap } from 'rxjs';

@Component({
  selector: 'edit-table',
  templateUrl: './edit-table.component.html',
  styleUrls: ['./edit-table.component.scss'],
})
export class EditTableComponent implements OnInit {
  form: FormGroup;
  tableId: string;

  constructor(
    readonly tableFacade: TableFacade,
    private readonly formBuilder: FormBuilder,
    private readonly router: Router,
    private readonly route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.tableId = this.route.snapshot.paramMap.get('tableId');
    this.tableFacade
      .byId$(this.tableId)
      .pipe(
        take(1),
        tap(table => {
          this.form = new FormGroup({
            tableNumber: new FormControl(
              table.tableNumber,
              Validators.required
            ),
            capacity: new FormControl(table.capacity, Validators.required),
            description: new FormControl(table.description, []),
          });
        })
      )
      .subscribe();
  }

  navigateToList() {
    this.router.navigate(['tables']);
  }
}
