import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IonicModule } from '@ionic/angular';
import { FormsModule } from '@angular/forms';
import { TabsContainerPage } from './pages/tabs-container/tabs-container-page.component';
import { LoginButtonComponent } from './components/login-button/login-button.component';
import { LogoutButtonComponent } from './components/logout-button/logout-button.component';
import { ProfileComponent } from './components/profile/profile.component';
import { LoginPage } from './pages/login/login.page';
import { HomePage } from './pages/home/home.page';
import { LocationCoreModule } from '../locations-core/location-core.module';
import { LogoutPage } from './pages/logout/logout.page';

@NgModule({
  imports: [CommonModule, FormsModule, IonicModule, LocationCoreModule],
  declarations: [
    TabsContainerPage,
    LoginPage,
    LogoutPage,
    HomePage,
    LoginButtonComponent,
    LogoutButtonComponent,
    ProfileComponent,
  ],
})
export class MainModule {}
