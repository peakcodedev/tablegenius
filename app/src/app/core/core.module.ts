import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { DesignModule } from "../design/design.module";
import { AppState } from "./app-state";
import { BlobStoragePipe } from "./pipes/blob-storage.pipe";
import { AuthenticationService } from "./services/authentication.service";

@NgModule({
  declarations: [BlobStoragePipe],
  exports: [BlobStoragePipe],
  imports: [CommonModule, DesignModule],
  providers: [AppState, AuthenticationService, BlobStoragePipe],
})
export class CoreModule {}
