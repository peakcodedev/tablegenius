import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { EndpointBuilder } from '../../core/endpoints/endpoint-builder';
import { IApiResponse } from '../../domain/api-response';
import { ITableModel } from '../../models/table-model';
import { IArea } from '../../domain/area';
import { IAreaModel } from '../../models/area-model';

@Injectable({
  providedIn: 'root',
})
export class AreaService {
  private static endpoint = EndpointBuilder.buildDomainEndpoint('areas');
  constructor(private readonly httpClient: HttpClient) {}

  getAreas(): Observable<IApiResponse<IArea[]>> {
    return this.httpClient.get<IApiResponse<IArea[]>>(AreaService.endpoint);
  }

  createArea(model: ITableModel): Observable<IApiResponse<IArea>> {
    return this.httpClient.post<IApiResponse<IArea>>(
      AreaService.endpoint,
      model
    );
  }

  updateArea(
    areaId: string,
    model: IAreaModel
  ): Observable<IApiResponse<IArea>> {
    return this.httpClient.patch<IApiResponse<IArea>>(
      AreaService.singleEndpoint(areaId),
      model
    );
  }

  deleteArea(areaId: string): Observable<boolean> {
    return this.httpClient.delete<boolean>(AreaService.singleEndpoint(areaId));
  }

  private static singleEndpoint(id: string) {
    return AreaService.endpoint + '/' + id;
  }
}
