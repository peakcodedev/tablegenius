import { Component, OnInit } from '@angular/core';
import { TableFacade } from '../../../tables-core/state/table.facade';
import { Router } from '@angular/router';

@Component({
  selector: 'tables-list',
  templateUrl: './tables-list.component.html',
  styleUrls: ['./tables-list.component.scss'],
})
export class TablesListComponent implements OnInit {
  cols = [
    { title: 'Tischnummer', field: 'tableNumber' },
    { title: 'Grösse', field: 'capacity' },
    { title: 'Bereich', field: '' },
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
      onClick: (id: string) => this.tableFacade.deleteTable(id),
    },
  ];

  constructor(
    readonly tableFacade: TableFacade,
    private readonly router: Router
  ) {}

  ngOnInit(): void {
    this.tableFacade.loadTables();
  }

  navigateToAddForm() {
    this.router.navigate(['tables/add']);
  }

  navigateToEditForm(id: string) {
    this.router.navigate(['tables/' + id]);
  }
}
