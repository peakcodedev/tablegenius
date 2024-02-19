import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { EndpointBuilder } from '../../core/endpoints/endpoint-builder';
import { IReservationAssignmentModel } from '../../models/reservation-assignment-model';
import { IApiResponse } from '../../domain/api-response';
import { IReservation } from '../../domain/reservation';

@Injectable({
  providedIn: 'root',
})
export class ReservationAssignmentsService {
  private static endpoint = EndpointBuilder.buildDomainEndpoint(
    'reservationAssignments'
  );
  constructor(private readonly httpClient: HttpClient) {}

  deleteReservationAssignment(id: string): Observable<boolean> {
    return this.httpClient.delete<boolean>(
      ReservationAssignmentsService.singleEndpoint(id)
    );
  }

  createReservationAssignment(
    model: IReservationAssignmentModel
  ): Observable<IApiResponse<IReservation>> {
    return this.httpClient.post<IApiResponse<IReservation>>(
      ReservationAssignmentsService.endpoint,
      model
    );
  }

  private static singleEndpoint(id: string) {
    return ReservationAssignmentsService.endpoint + '/' + id;
  }
}
