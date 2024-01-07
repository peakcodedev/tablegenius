import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { LocationFacade } from '../../../locations-core/state/location.facade';
import { ConfirmationService } from 'primeng/api';

@Component({
  selector: 'location-assignments-list',
  templateUrl: './location-assignments-list.component.html',
  providers: [ConfirmationService],
})
export class LocationAssignmentsListComponent implements OnInit {
  cols = [{ title: 'Benutzer', field: 'mail' }];
  locationId: string;
  tableActions = [
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
    private readonly route: ActivatedRoute,
    private readonly confirmationService: ConfirmationService
  ) {}

  ngOnInit(): void {
    this.locationId = this.route.snapshot.paramMap.get('locationId');
    this.locationFacade.loadLocationAssignments(this.locationId);
  }

  navigateToAddForm() {
    this.router.navigate(['locations/' + this.locationId + '/assignments/add']);
  }

  navigateToList() {
    this.router.navigate(['locations']);
  }

  private displayDeleteConfirmDialog(id: string) {
    this.confirmationService.confirm({
      message:
        'Bist du dir sicher, dass du diesen Benutzer von Restaurant entfernen möchtest?',
      header: 'Benutzerzuweisung entfernen',
      acceptLabel: 'Ja',
      rejectLabel: 'Nein',
      icon: 'pi pi-info-circle',
      accept: () => {
        this.locationFacade.deleteLocationAssignment(id);
      },
      reject: () => {
        this.confirmationService.close();
      },
    });
  }
}
