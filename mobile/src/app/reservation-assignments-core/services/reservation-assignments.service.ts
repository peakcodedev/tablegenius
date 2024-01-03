import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { EndpointBuilder } from '../../core/endpoints/endpoint-builder';

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

  private static singleEndpoint(id: string) {
    return ReservationAssignmentsService.endpoint + '/' + id;
  }
}
