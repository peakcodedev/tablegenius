import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { EndpointBuilder } from '../../core/endpoints/endpoint-builder';
import { IApiResponse } from '../../domain/api-response';
import { ILocation } from '../../domain/location';

@Injectable({
  providedIn: 'root',
})
export class UserLocationsService {
  private static endpoint = EndpointBuilder.buildDomainEndpoint('locations/me');
  constructor(private readonly httpClient: HttpClient) {}

  getLocations(): Observable<IApiResponse<ILocation[]>> {
    return this.httpClient.get<IApiResponse<ILocation[]>>(
      UserLocationsService.endpoint
    );
  }
}
