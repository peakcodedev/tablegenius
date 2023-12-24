import { Pipe, PipeTransform } from "@angular/core";
import { environment } from "../../../environments/environment";

@Pipe({
  name: "blobStorage",
})
export class BlobStoragePipe implements PipeTransform {
  transform(blobObject: string, folder: string): string {
    return environment.blobUrl + "/" + folder + "/" + blobObject;
  }
}
