import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { Router } from '@angular/router';
import { AreasHelper } from '../../../areas-core/helpers/areas.helper';
import { Observable } from 'rxjs';
import { AreaSlotFacade } from '../../../area-slots-core/state/area-slot.facade';

@Component({
  selector: 'add-area-slot',
  templateUrl: './add-area-slot.component.html',
  styleUrls: ['./add-area-slot.component.scss'],
})
export class AddAreaSlotComponent implements OnInit {
  form: FormGroup;
  areas: Observable<any[]>;

  constructor(
    readonly areaSlotFacade: AreaSlotFacade,
    private readonly formBuilder: FormBuilder,
    private readonly router: Router,
    readonly areasHelper: AreasHelper
  ) {}

  ngOnInit(): void {
    this.form = new FormGroup({
      name: new FormControl('', Validators.required),
      type: new FormControl('', Validators.required),
      length: new FormControl('', []),
      start: new FormControl('', []),
      end: new FormControl('', []),
      areaId: new FormControl(null, Validators.required),
    });
    this.areas = this.areasHelper.availableAreas();
  }

  navigateToList() {
    this.router.navigate(['areaSlots']);
  }
}
