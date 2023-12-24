import { Component } from "@angular/core";
import { AuthService } from "@auth0/auth0-angular";

@Component({
  selector: "app-current-user",
  templateUrl: "./current-user.component.html",
  styleUrls: ["./current-user.component.scss"],
})
export class CurrentUserComponent {
  constructor(readonly authService: AuthService) {}
}
