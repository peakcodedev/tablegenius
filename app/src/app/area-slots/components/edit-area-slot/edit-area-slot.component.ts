import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable, take, tap } from 'rxjs';
import { AreaSlotFacade } from '../../../area-slots-core/state/area-slot.facade';
import { AreasHelper } from '../../../areas-core/helpers/areas.helper';
import { AreaSlotTypesHelper } from '../../../area-slots-core/helpers/area-slot-types.helper';

@Component({
  selector: 'edit-area-slot',
  templateUrl: './edit-area-slot.component.html',
  styleUrls: ['./edit-area-slot.component.scss'],
})
export class EditAreaSlotComponent implements OnInit {
  form: FormGroup;
  areaSlotId: string;
  areas: Observable<any[]>;
  types: any[];

  constructor(
    readonly areaSlotFacade: AreaSlotFacade,
    private readonly formBuilder: FormBuilder,
    private readonly router: Router,
    private readonly route: ActivatedRoute,
    private readonly areasHelper: AreasHelper,
    private readonly typesHelper: AreaSlotTypesHelper
  ) {}

  ngOnInit(): void {
    this.areaSlotId = this.route.snapshot.paramMap.get('areaSlotId');
    this.areas = this.areasHelper.availableAreas();
    this.types = this.typesHelper.types();
    this.areaSlotFacade
      .byId$(this.areaSlotId)
      .pipe(
        take(1),
        tap(areaSlot => {
          this.form = new FormGroup({
            name: new FormControl(areaSlot.name, Validators.required),
            type: new FormControl(areaSlot.type, Validators.required),
            length: new FormControl(areaSlot.length, []),
            start: new FormControl(areaSlot.start, []),
            end: new FormControl(areaSlot.end, []),
            areaId: new FormControl(areaSlot.areaId, Validators.required),
          });
        })
      )
      .subscribe();
  }

  navigateToList() {
    this.router.navigate(['areaSlots']);
  }
}
