import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavigationComponent } from './components/navigation/navigation.component';
import { MenubarModule } from 'primeng/menubar';
import { CurrentUserComponent } from './components/current-user/current-user.component';

@NgModule({
  declarations: [NavigationComponent, CurrentUserComponent],
  exports: [NavigationComponent, CurrentUserComponent],
  imports: [CommonModule, MenubarModule],
})
export class NavigationModule {}
