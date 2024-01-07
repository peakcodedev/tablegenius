import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { EndpointBuilder } from '../../core/endpoints/endpoint-builder';
import { IApiResponse } from '../../domain/api-response';
import { ILocationModel } from '../../models/location-model';
import { ILocationAssignment } from '../../domain/location-assignment';

@Injectable({
  providedIn: 'root',
})
export class LocationAssignmentService {
  private static endpoint = EndpointBuilder.buildDomainEndpoint(
    'locationAssignments'
  );
  constructor(private readonly httpClient: HttpClient) {}

  getLocationAssignments(
    locationId: string
  ): Observable<IApiResponse<ILocationAssignment[]>> {
    return this.httpClient.get<IApiResponse<ILocationAssignment[]>>(
      LocationAssignmentService.endpoint
    );
  }

  createLocationAssignment(
    model: ILocationModel
  ): Observable<IApiResponse<ILocationAssignment>> {
    return this.httpClient.post<IApiResponse<ILocationAssignment>>(
      LocationAssignmentService.endpoint,
      model
    );
  }

  updateLocationAssignment(
    locationAssignmentId: string,
    model: ILocationModel
  ): Observable<IApiResponse<ILocationAssignment>> {
    return this.httpClient.patch<IApiResponse<ILocationAssignment>>(
      LocationAssignmentService.singleEndpoint(locationAssignmentId),
      model
    );
  }

  deleteLocationAssignment(locationAssignmentId: string): Observable<boolean> {
    return this.httpClient.delete<boolean>(
      LocationAssignmentService.singleEndpoint(locationAssignmentId)
    );
  }

  private static singleEndpoint(id: string) {
    return LocationAssignmentService.endpoint + '/' + id;
  }
}
