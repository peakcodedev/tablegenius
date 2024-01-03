import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { EndpointBuilder } from '../../core/endpoints/endpoint-builder';
import { IApiResponse } from '../../domain/api-response';
import { ITable } from '../../domain/table';
import { ITableModel } from '../../models/table-model';

@Injectable({
  providedIn: 'root',
})
export class TableService {
  private static endpoint = EndpointBuilder.buildDomainEndpoint('tables');
  constructor(private readonly httpClient: HttpClient) {}

  getTables(): Observable<IApiResponse<ITable[]>> {
    return this.httpClient.get<IApiResponse<ITable[]>>(TableService.endpoint);
  }

  createTable(model: ITableModel): Observable<IApiResponse<ITable>> {
    return this.httpClient.post<IApiResponse<ITable>>(
      TableService.endpoint,
      model
    );
  }

  updateTable(
    tableId: string,
    model: ITableModel
  ): Observable<IApiResponse<ITable>> {
    return this.httpClient.patch<IApiResponse<ITable>>(
      TableService.singleEndpoint(tableId),
      model
    );
  }

  deleteTable(tableId: string): Observable<boolean> {
    return this.httpClient.delete<boolean>(
      TableService.singleEndpoint(tableId)
    );
  }

  private static singleEndpoint(id: string) {
    return TableService.endpoint + '/' + id;
  }
}
