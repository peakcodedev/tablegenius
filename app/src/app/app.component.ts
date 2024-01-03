import { Component, OnInit } from '@angular/core';
import { AuthService } from '@auth0/auth0-angular';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export class AppComponent implements OnInit {
  constructor(private readonly authService: AuthService) {}

  ngOnInit(): void {
    this.authService.error$.subscribe(error => {
      if (error.message !== 'Login required') {
        this.authService.getAccessTokenWithPopup().subscribe();
      }
    });
  }
}
