import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { EndpointBuilder } from '../../core/endpoints/endpoint-builder';
import { IApiResponse } from '../../domain/api-response';
import { ILocation } from '../../domain/location';
import { ILocationModel } from '../../models/location-model';

@Injectable({
  providedIn: 'root',
})
export class LocationService {
  private static endpoint = EndpointBuilder.buildDomainEndpoint('locations');
  constructor(private readonly httpClient: HttpClient) {}

  getLocations(): Observable<IApiResponse<ILocation[]>> {
    return this.httpClient.get<IApiResponse<ILocation[]>>(
      LocationService.endpoint
    );
  }

  createLocation(model: ILocationModel): Observable<IApiResponse<ILocation>> {
    return this.httpClient.post<IApiResponse<ILocation>>(
      LocationService.endpoint,
      model
    );
  }

  updateLocation(
    locationId: string,
    model: ILocationModel
  ): Observable<IApiResponse<ILocation>> {
    return this.httpClient.patch<IApiResponse<ILocation>>(
      LocationService.singleEndpoint(locationId),
      model
    );
  }

  deleteLocation(locationId: string): Observable<boolean> {
    return this.httpClient.delete<boolean>(
      LocationService.singleEndpoint(locationId)
    );
  }

  private static singleEndpoint(id: string) {
    return LocationService.endpoint + '/' + id;
  }
}
