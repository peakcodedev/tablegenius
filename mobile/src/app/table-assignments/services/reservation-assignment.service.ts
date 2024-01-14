import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { EndpointBuilder } from '../../core/endpoints/endpoint-builder';
import { IApiResponse } from '../../domain/api-response';
import { IReservation } from '../../domain/reservation';
import { IReservationAssignmentModel } from '../../models/reservation-assignment-model';

@Injectable({
  providedIn: 'root',
})
export class ReservationAssignmentService {
  private static endpoint = EndpointBuilder.buildDomainEndpoint(
    'reservationAssignments'
  );
  constructor(private readonly httpClient: HttpClient) {}

  createReservationAssignment(
    model: IReservationAssignmentModel
  ): Observable<IApiResponse<IReservation>> {
    return this.httpClient.post<IApiResponse<IReservation>>(
      ReservationAssignmentService.endpoint,
      model
    );
  }

  private static singleEndpoint(id: string) {
    return ReservationAssignmentService.endpoint + '/' + id;
  }
}
