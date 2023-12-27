import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CardModule } from 'primeng/card';
import { ButtonModule } from 'primeng/button';
import { DesignModule } from '../../design/design.module';
import { LoginPage } from './pages/login/login.page';
import { LogoutPage } from './pages/logout/logout.page';
import { HomePage } from './pages/home/home.page';

@NgModule({
  declarations: [LoginPage, LogoutPage, HomePage],
  exports: [],
  imports: [CommonModule, DesignModule, CardModule, ButtonModule, DesignModule],
})
export class MainModule {}
