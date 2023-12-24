import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AreaFacade } from '../../../areas-core/state/area.facade';

@Component({
  selector: 'areas-list',
  templateUrl: './areas-list.component.html',
  styleUrls: ['./areas-list.component.scss'],
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
      onClick: (id: string) => this.areaFacade.deleteArea(id),
    },
  ];

  constructor(
    readonly areaFacade: AreaFacade,
    private readonly router: Router
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
}
