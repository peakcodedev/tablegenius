import { Component, Input } from '@angular/core';

import { ModalController } from '@ionic/angular';
import { Observable } from 'rxjs';
import { ITableWithStatus } from '../../../domain/table';

@Component({
  selector: 'table-selection-modal',
  templateUrl: 'table-selection-modal.component.html',
  styleUrls: ['table-selection-modal.component.scss'],
})
export class TableSelectionModalComponent {
  @Input() tables$: Observable<ITableWithStatus[]>;
  @Input() selectedTables: ITableWithStatus[] = [];

  constructor(private modalCtrl: ModalController) {}

  cancel() {
    return this.modalCtrl.dismiss(null, 'cancel');
  }

  onTableSelected(table: ITableWithStatus) {
    return this.modalCtrl.dismiss(table, 'confirm');
  }

  tableIsSelected(table: ITableWithStatus): boolean {
    console.error(table);
    if (table.taken) return true;
    return (
      this.selectedTables && this.selectedTables.some(st => st.id === table.id)
    );
  }
}
