import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IonicModule } from '@ionic/angular';
import { FormsModule } from '@angular/forms';
import { HomePage } from './pages/home/home.page';

import { MainRoutingModule } from './main-routing.module';
import { LoginButtonComponent } from './components/login-button/login-button.component';
import { LogoutButtonComponent } from './components/logout-button/logout-button.component';
import { ProfileComponent } from './components/profile/profile.component';

@NgModule({
  imports: [CommonModule, FormsModule, IonicModule, MainRoutingModule],
  declarations: [
    HomePage,
    LoginButtonComponent,
    LogoutButtonComponent,
    ProfileComponent,
  ],
})
export class MainModule {}
