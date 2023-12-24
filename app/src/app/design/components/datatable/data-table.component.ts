import { Component, EventEmitter, Input, Output } from "@angular/core";
import { SortEvent } from "primeng/api";

@Component({
  selector: "app-datatable",
  templateUrl: "./data-table.component.html",
  styleUrls: ["./data-table.component.scss"],
})
export class DataTableComponent {
  @Input() loading = false;
  @Input() hasActions = false;
  @Input() hasConfirmDialogs = false;
  @Input() data: any[] = [];
  @Input() cols: Col[] = [];
  @Input() actions: Action[] = [];
  @Input() filterFields: string[] = ["id"];
  @Input() initialSortField = "";
  @Output() clicked = new EventEmitter();

  customSort(event: SortEvent) {
    event.data.sort((data1, data2) => {
      let value1 = data1[event.field.replace("DateString", "Date")];
      let value2 = data2[event.field.replace("DateString", "Date")];
      let result = null;

      if (value1 == null && value2 != null) result = -1;
      else if (value1 != null && value2 == null) result = 1;
      else if (value1 == null && value2 == null) result = 0;
      else if (typeof value1 === "string" && typeof value2 === "string")
        result = value1.localeCompare(value2);
      else result = value1 < value2 ? -1 : value1 > value2 ? 1 : 0;

      return event.order * result;
    });
  }
}

type Col = { title: string; field: string };
type Action = {
  label: string;
  icon: string;
  style: string;
  onClick(id: string): void;
};
