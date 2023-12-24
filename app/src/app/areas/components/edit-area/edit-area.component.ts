import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { take, tap } from 'rxjs';
import { AreaFacade } from '../../../areas-core/state/area.facade';

@Component({
  selector: 'edit-area',
  templateUrl: './edit-area.component.html',
  styleUrls: ['./edit-area.component.scss'],
})
export class EditAreaComponent implements OnInit {
  form: FormGroup;
  areaId: string;

  constructor(
    readonly areaFacade: AreaFacade,
    private readonly formBuilder: FormBuilder,
    private readonly router: Router,
    private readonly route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.areaId = this.route.snapshot.paramMap.get('areaId');
    this.areaFacade
      .byId$(this.areaId)
      .pipe(
        take(1),
        tap(area => {
          this.form = new FormGroup({
            tableNumber: new FormControl(area.tableNumber, Validators.required),
            capacity: new FormControl(area.capacity, Validators.required),
            description: new FormControl(area.description, []),
          });
        })
      )
      .subscribe();
  }

  navigateToList() {
    this.router.navigate(['areas']);
  }
}
