import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AppState } from './app-state';
import { AuthenticationService } from './services/authentication.service';
import { NgxsModule } from '@ngxs/store';
import { CoreState } from './state/core.state';
import { CoreFacade } from './state/core.facade';

@NgModule({
  imports: [CommonModule, NgxsModule.forFeature([CoreState])],
  providers: [AppState, AuthenticationService, CoreFacade],
})
export class CoreModule {}
