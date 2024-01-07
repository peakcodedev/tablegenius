import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { EndpointBuilder } from '../../core/endpoints/endpoint-builder';
import { IApiResponse } from '../../domain/api-response';
import { ILocationAssignment } from '../../domain/location-assignment';
import { ILocationAssignmentModel } from '../../models/location-assignment-model';

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
      LocationAssignmentService.singleEndpoint(locationId)
    );
  }

  createLocationAssignment(
    model: ILocationAssignmentModel
  ): Observable<IApiResponse<ILocationAssignment>> {
    return this.httpClient.post<IApiResponse<ILocationAssignment>>(
      LocationAssignmentService.endpoint,
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
