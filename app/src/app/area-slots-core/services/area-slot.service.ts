import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { EndpointBuilder } from '../../core/endpoints/endpoint-builder';
import { IApiResponse } from '../../domain/api-response';
import { IAreaSlot } from '../../domain/area-slot';
import { IAreaSlotModel } from '../../models/area-slot-model';

@Injectable({
  providedIn: 'root',
})
export class AreaSlotService {
  private static endpoint = EndpointBuilder.buildDomainEndpoint('areaSlots');
  constructor(private readonly httpClient: HttpClient) {}

  getAreaSlots(): Observable<IApiResponse<IAreaSlot[]>> {
    return this.httpClient.get<IApiResponse<IAreaSlot[]>>(
      AreaSlotService.endpoint
    );
  }

  createAreaSlot(model: IAreaSlotModel): Observable<IApiResponse<IAreaSlot>> {
    return this.httpClient.post<IApiResponse<IAreaSlot>>(
      AreaSlotService.endpoint,
      model
    );
  }

  updateAreaSlot(
    areaSlotId: string,
    model: IAreaSlotModel
  ): Observable<IApiResponse<IAreaSlot>> {
    return this.httpClient.patch<IApiResponse<IAreaSlot>>(
      AreaSlotService.singleEndpoint(areaSlotId),
      model
    );
  }

  deleteAreaSlot(areaSlotId: string): Observable<boolean> {
    return this.httpClient.delete<boolean>(
      AreaSlotService.singleEndpoint(areaSlotId)
    );
  }

  private static singleEndpoint(id: string) {
    return AreaSlotService.endpoint + '/' + id;
  }
}
