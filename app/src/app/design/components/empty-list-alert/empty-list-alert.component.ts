import { Component, Input, OnInit } from "@angular/core";
import { Message } from "primeng/api";

@Component({
  selector: "empty-list-alert",
  templateUrl: "./empty-list-alert.component.html",
  styleUrls: ["./empty-list-alert.component.scss"],
})
export class EmptyListAlertComponent implements OnInit {
  @Input()
  summary: string;

  @Input()
  text: string;

  messages: Message[];

  constructor() {}

  ngOnInit(): void {
    this.messages = [
      {
        severity: "info",
        summary: this.summary,
        detail: this.text,
        closable: false,
        sticky: true,
      },
    ];
  }
}
