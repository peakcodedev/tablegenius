import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ConfirmationService } from 'primeng/api';
import { map, Observable } from 'rxjs';
import { AreaFacade } from '../../../areas-core/state/area.facade';
import { AsyncPipe } from '@angular/common';
import { IAreaSlot } from '../../../domain/area-slot';
import { AreaSlotFacade } from '../../../area-slots-core/state/area-slot.facade';

@Component({
  selector: 'area-slots-list',
  templateUrl: './area-slots-list.component.html',
  styleUrls: ['./area-slots-list.component.scss'],
  providers: [ConfirmationService],
})
export class AreaSlotsListComponent implements OnInit {
  cols = [
    { title: 'Bereich', field: 'areaName' },
    { title: 'Name', field: 'name' },
    { title: 'Typ', field: 'typeString' },
  ];
  tableActions = [
    {
      label: 'Bearbeiten',
      icon: 'pencil',
      style: 'text',
      onClick: (id: string) => this.navigateToEditForm(id),
    },
    {
      label: 'Löschen',
      icon: 'trash',
      style: 'text',
      onClick: (id: string) => this.displayDeleteConfirmDialog(id),
    },
  ];
  data$: Observable<IAreaSlot[]>;

  constructor(
    readonly areaSlotFacade: AreaSlotFacade,
    private readonly router: Router,
    private readonly confirmationService: ConfirmationService,
    private readonly areaFacade: AreaFacade,
    private readonly asyncPipe: AsyncPipe
  ) {}

  ngOnInit(): void {
    this.areaSlotFacade.loadAreaSlots();
    this.data$ = this.areaSlotFacade.areaSlotsList.pipe(
      map(areaSlots =>
        areaSlots.map(areaSlot => ({
          ...areaSlot,
          areaName: this.asyncPipe.transform(
            this.areaFacade.byId$(areaSlot.areaId)
          )?.name,
        }))
      )
    );
  }

  navigateToAddForm() {
    this.router.navigate(['areaSlots/add']);
  }

  navigateToEditForm(id: string) {
    this.router.navigate(['areaSlots/' + id]);
  }

  private displayDeleteConfirmDialog(id: string) {
    this.confirmationService.confirm({
      message:
        'Bist du dir sicher, dass du diesen Zeitbereich löschen möchtest?',
      header: 'Zeitbereich löschen',
      acceptLabel: 'Ja',
      rejectLabel: 'Nein',
      icon: 'pi pi-info-circle',
      accept: () => {
        this.areaSlotFacade.deleteAreaSlot(id);
      },
      reject: () => {
        this.confirmationService.close();
      },
    });
  }
}
