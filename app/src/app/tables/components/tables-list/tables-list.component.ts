import { Component, OnInit } from '@angular/core';
import { TableFacade } from '../../../tables-core/state/table.facade';
import { Router } from '@angular/router';
import { ConfirmationService } from 'primeng/api';
import { map, Observable } from 'rxjs';
import { ITable } from '../../../domain/table';
import { AreaFacade } from '../../../areas-core/state/area.facade';
import { AsyncPipe } from '@angular/common';

@Component({
  selector: 'tables-list',
  templateUrl: './tables-list.component.html',
  styleUrls: ['./tables-list.component.scss'],
  providers: [ConfirmationService],
})
export class TablesListComponent implements OnInit {
  cols = [
    { title: 'Tischnummer', field: 'tableNumber' },
    { title: 'Grösse', field: 'capacity' },
    { title: 'Bereich', field: 'areaName' },
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
  data$: Observable<ITable[]>;

  constructor(
    readonly tableFacade: TableFacade,
    private readonly router: Router,
    private readonly confirmationService: ConfirmationService,
    private readonly areaFacade: AreaFacade,
    private readonly asyncPipe: AsyncPipe
  ) {}

  ngOnInit(): void {
    this.tableFacade.loadTables();
    this.data$ = this.tableFacade.tablesList.pipe(
      map(tables =>
        tables.map(table => ({
          ...table,
          areaName: this.asyncPipe.transform(
            this.areaFacade.byId$(table.areaId)
          )?.name,
        }))
      )
    );
  }

  navigateToAddForm() {
    this.router.navigate(['tables/add']);
  }

  navigateToEditForm(id: string) {
    this.router.navigate(['tables/' + id]);
  }

  private displayDeleteConfirmDialog(id: string) {
    this.confirmationService.confirm({
      message: 'Bist du dir sicher, dass du diesen Tisch löschen möchtest?',
      header: 'Tisch löschen',
      acceptLabel: 'Ja',
      rejectLabel: 'Nein',
      icon: 'pi pi-info-circle',
      accept: () => {
        this.tableFacade.deleteTable(id);
      },
      reject: () => {},
    });
  }
}
