import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DesignModule } from '../design/design.module';
import { AppState } from './app-state';
import { BlobStoragePipe } from './pipes/blob-storage.pipe';
import { AuthenticationService } from './services/authentication.service';
import { NgxsModule } from '@ngxs/store';
import { CoreState } from './state/core.state';
import { CoreFacade } from './state/core.facade';

@NgModule({
  declarations: [BlobStoragePipe],
  exports: [BlobStoragePipe],
  imports: [CommonModule, DesignModule, NgxsModule.forFeature([CoreState])],
  providers: [AppState, AuthenticationService, BlobStoragePipe, CoreFacade],
})
export class CoreModule {}
