import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FooterComponent } from "./components/footer/footer.component";
import { HeaderComponent } from "./components/header/header.component";
import { EmptyListAlertComponent } from "./components/empty-list-alert/empty-list-alert.component";
import { MessageModule } from "primeng/message";
import { MessagesModule } from "primeng/messages";
import { DataTableComponent } from "./components/datatable/data-table.component";
import { ConfirmDialogModule } from "primeng/confirmdialog";
import { TableModule } from "primeng/table";
import { ButtonComponent } from "./components/button/button.component";
import { InputTextModule } from "primeng/inputtext";
import { ProgressBarModule } from "primeng/progressbar";
import { LoadingSpinnerComponent } from "./components/loading-spinner/loading-spinner.component";
import { UploadFileComponent } from "./components/upload-file/upload-file.component";

@NgModule({
  declarations: [
    FooterComponent,
    HeaderComponent,
    EmptyListAlertComponent,
    LoadingSpinnerComponent,
    DataTableComponent,
    ButtonComponent,
    UploadFileComponent,
  ],
  exports: [
    FooterComponent,
    HeaderComponent,
    EmptyListAlertComponent,
    LoadingSpinnerComponent,
    DataTableComponent,
    ButtonComponent,
    UploadFileComponent,
  ],
  imports: [
    CommonModule,
    MessageModule,
    MessagesModule,
    ConfirmDialogModule,
    ProgressBarModule,
    TableModule,
    InputTextModule,
  ],
})
export class DesignModule {}
