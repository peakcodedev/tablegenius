import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { TableFacade } from '../../../tables-core/state/table.facade';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable, take, tap } from 'rxjs';
import { AreasHelper } from '../../../areas-core/helpers/areas.helper';

@Component({
  selector: 'edit-table',
  templateUrl: './edit-table.component.html',
  styleUrls: ['./edit-table.component.scss'],
})
export class EditTableComponent implements OnInit {
  form: FormGroup;
  tableId: string;
  areas: Observable<any[]>;

  constructor(
    readonly tableFacade: TableFacade,
    private readonly formBuilder: FormBuilder,
    private readonly router: Router,
    private readonly route: ActivatedRoute,
    private readonly areasHelper: AreasHelper
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
            areaId: new FormControl(table.areaId, Validators.required),
            description: new FormControl(table.description, []),
          });
        })
      )
      .subscribe();
    this.areas = this.areasHelper.availableAreas();
  }

  navigateToList() {
    this.router.navigate(['tables']);
  }
}
