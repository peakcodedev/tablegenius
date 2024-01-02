import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { EndpointBuilder } from '../../core/endpoints/endpoint-builder';
import { IApiResponse } from '../../domain/api-response';
import { IDateFilterModel } from '../../models/date-filter-model';
import { IReservationAssignment } from '../../domain/reservation-assignment';
import { ITable } from '../../domain/table';

@Injectable({
  providedIn: 'root',
})
export class ReservationAssignmentsOverviewService {
  private static endpoint = EndpointBuilder.buildDomainEndpoint('areaSlots');
  constructor(private readonly httpClient: HttpClient) {}

  getReservations(
    areaSlotId: string,
    model: IDateFilterModel
  ): Observable<IApiResponse<IReservationAssignment[]>> {
    return this.httpClient.post<IApiResponse<IReservationAssignment[]>>(
      ReservationAssignmentsOverviewService.singleEndpoint(areaSlotId) +
        '/reservations',
      model
    );
  }

  getFreeTables(
    areaSlotId: string,
    model: IDateFilterModel
  ): Observable<IApiResponse<ITable[]>> {
    return this.httpClient.post<IApiResponse<ITable[]>>(
      ReservationAssignmentsOverviewService.singleEndpoint(areaSlotId) +
        '/freeTables',
      model
    );
  }

  private static singleEndpoint(id: string) {
    return ReservationAssignmentsOverviewService.endpoint + '/' + id;
  }
}
