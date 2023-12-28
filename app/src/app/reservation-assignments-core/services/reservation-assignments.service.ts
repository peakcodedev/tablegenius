import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { EndpointBuilder } from '../../core/endpoints/endpoint-builder';
import { IApiResponse } from '../../domain/api-response';
import { IReservation } from '../../domain/reservation';
import { IReservationModel } from '../../models/reservation-model';

@Injectable({
  providedIn: 'root',
})
export class ReservationAssignmentsService {
  private static endpoint = EndpointBuilder.buildDomainEndpoint(
    'reservationsAssignments'
  );
  constructor(private readonly httpClient: HttpClient) {}

  getReservations(): Observable<IApiResponse<IReservation[]>> {
    return this.httpClient.get<IApiResponse<IReservation[]>>(
      ReservationAssignmentsService.endpoint
    );
  }

  getUnassignedReservations(): Observable<IApiResponse<IReservation[]>> {
    return this.httpClient.get<IApiResponse<IReservation[]>>(
      ReservationAssignmentsService.endpoint + '/unassigned'
    );
  }

  createReservation(
    model: IReservationModel
  ): Observable<IApiResponse<IReservation>> {
    return this.httpClient.post<IApiResponse<IReservation>>(
      ReservationAssignmentsService.endpoint,
      model
    );
  }

  updateReservation(
    reservationId: string,
    model: IReservationModel
  ): Observable<IApiResponse<IReservation>> {
    return this.httpClient.patch<IApiResponse<IReservation>>(
      ReservationAssignmentsService.singleEndpoint(reservationId),
      model
    );
  }

  deleteReservation(reservationId: string): Observable<boolean> {
    return this.httpClient.delete<boolean>(
      ReservationAssignmentsService.singleEndpoint(reservationId)
    );
  }

  private static singleEndpoint(id: string) {
    return ReservationAssignmentsService.endpoint + '/' + id;
  }
}
