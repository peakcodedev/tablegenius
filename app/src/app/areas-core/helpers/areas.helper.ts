import { Injectable } from '@angular/core';
import { map, mergeMap, Observable, take, toArray } from 'rxjs';
import { AreaFacade } from '../state/area.facade';
import { IArea } from '../../domain/area';

@Injectable()
export class AreasHelper {
  constructor(private readonly areaFacade: AreaFacade) {}
  availableAreas(): Observable<any[]> {
    return this.getAllAreas();
  }

  private getAllAreas(): Observable<any[]> {
    return this.areaFacade.areas.pipe(
      take(1),
      mergeMap(areas => areas),
      map((item: IArea) => {
        return {
          name: item.name,
          value: item.id,
        };
      }),
      toArray()
    );
  }
}
