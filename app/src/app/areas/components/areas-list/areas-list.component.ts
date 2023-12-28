import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AreaFacade } from '../../../areas-core/state/area.facade';
import { ConfirmationService } from 'primeng/api';

@Component({
  selector: 'areas-list',
  templateUrl: './areas-list.component.html',
  styleUrls: ['./areas-list.component.scss'],
  providers: [ConfirmationService],
})
export class AreasListComponent implements OnInit {
  cols = [{ title: 'Name', field: 'name' }];
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

  constructor(
    readonly areaFacade: AreaFacade,
    private readonly router: Router,
    private readonly confirmationService: ConfirmationService
  ) {}

  ngOnInit(): void {
    this.areaFacade.loadAreas();
  }

  navigateToAddForm() {
    this.router.navigate(['areas/add']);
  }

  navigateToEditForm(id: string) {
    this.router.navigate(['areas/' + id + '/edit']);
  }

  private displayDeleteConfirmDialog(id: string) {
    this.confirmationService.confirm({
      message: 'Bist du dir sicher, dass du diesen Bereich löschen möchtest?',
      header: 'Bereich löschen',
      acceptLabel: 'Ja',
      rejectLabel: 'Nein',
      icon: 'pi pi-info-circle',
      accept: () => {
        this.areaFacade.deleteArea(id);
      },
      reject: () => {
        this.confirmationService.close();
      },
    });
  }
}
