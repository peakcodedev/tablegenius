import { Component, Input, OnInit } from '@angular/core';
import { IReservation } from '../../../domain/reservation';
import { ITable } from '../../../domain/table';

@Component({
  selector: 'table-detail',
  templateUrl: './table-detail.component.html',
  styleUrls: ['./table-detail.component.scss'],
})
export class TableDetailComponent {
  @Input() table: ITable;
}
