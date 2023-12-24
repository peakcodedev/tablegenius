import { Component, Input } from "@angular/core";
import { catchError, Observable, tap } from "rxjs";
import { FileSnippet } from "../../../core/classes/file-snippet";
import { IApiResponse } from "../../../domain/api-response";

@Component({
  selector: "upload-file",
  templateUrl: "./upload-file.component.html",
  styleUrls: ["./upload-file.component.scss"],
})
export class UploadFileComponent {
  selectedFile: FileSnippet;
  @Input()
  id: string;
  @Input()
  showUploadedFile = true;
  @Input()
  extension = "pdf";
  @Input()
  actualFile: string;
  @Input()
  title: string;
  @Input()
  description: string;
  @Input()
  accept: string;
  @Input()
  name: string;
  @Input() public uploadFile: (
    file: File,
    id: string
  ) => Observable<IApiResponse<string>>;

  private onSuccess(url: string) {
    this.selectedFile.pending = false;
    this.selectedFile.status = "ok";
    this.actualFile = url;
  }

  private onError() {
    this.selectedFile.pending = false;
    this.selectedFile.status = "fail";
    this.selectedFile.src = "";
  }

  processFile(imageInput: any) {
    const file: File = imageInput.files[0];
    const reader = new FileReader();

    reader.addEventListener("load", (event: any) => {
      this.selectedFile = new FileSnippet(event.target.result, file);
      this.selectedFile.pending = true;

      return this.uploadFile(this.selectedFile.file, this.id)
        .pipe(
          tap((url) => {
            this.onSuccess(url.data);
          }),
          catchError((_) => {
            this.onError();
            return "";
          })
        )
        .subscribe();
    });

    reader.readAsDataURL(file);
  }
}
