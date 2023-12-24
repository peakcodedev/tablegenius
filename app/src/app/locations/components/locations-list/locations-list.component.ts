import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { LocationFacade } from '../../../locations-core/state/location.facade';

@Component({
  selector: 'locations-list',
  templateUrl: './locations-list.component.html',
  styleUrls: ['./locations-list.component.scss'],
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
      onClick: (id: string) => this.locationFacade.deleteLocation(id),
    },
  ];

  constructor(
    readonly locationFacade: LocationFacade,
    private readonly router: Router
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
}
