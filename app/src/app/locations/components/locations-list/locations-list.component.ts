import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { LocationFacade } from '../../../locations-core/state/location.facade';
import { ConfirmationService } from 'primeng/api';

@Component({
  selector: 'locations-list',
  templateUrl: './locations-list.component.html',
  styleUrls: ['./locations-list.component.scss'],
  providers: [ConfirmationService],
})
export class LocationsListComponent implements OnInit {
  cols = [
    { title: 'ID', field: 'id' },
    { title: 'Name', field: 'name' },
    { title: 'Adresse', field: 'address' },
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

  constructor(
    readonly locationFacade: LocationFacade,
    private readonly router: Router,
    private readonly confirmationService: ConfirmationService
  ) {}

  ngOnInit(): void {
    this.locationFacade.loadLocations();
  }

  navigateToAddForm() {
    this.router.navigate(['locations/add']);
  }

  navigateToEditForm(id: string) {
    this.router.navigate(['locations/' + id + '/edit']);
  }

  private displayDeleteConfirmDialog(id: string) {
    console.error(id);
    this.confirmationService.confirm({
      message:
        'Bist du dir sicher, dass du dieses Restaurant löschen möchtest?',
      header: 'Restaurant löschen',
      acceptLabel: 'Ja',
      rejectLabel: 'Nein',
      icon: 'pi pi-info-circle',
      accept: () => {
        this.locationFacade.deleteLocation(id);
      },
      reject: () => {
        this.confirmationService.close();
      },
    });
  }
}
