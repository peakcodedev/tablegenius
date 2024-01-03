import { Injectable } from '@angular/core';
import { IAreaSlot } from '../../domain/area-slot';
import { AppState } from '../../core/app-state';
import { AreaSlotState } from '../state/area-slot.state';

@Injectable()
export class AreaSlotsHelper {
  constructor(private readonly appState: AppState) {}
  availableAreaSlots(areaId: string): any[] {
    return this.getAllAreas(areaId);
  }

  private getAllAreas(areaId: string): any[] {
    let slots = this.appState.select(AreaSlotState.areaSlots);
    slots = areaId ? slots.filter(x => x.areaId === areaId) : slots;
    return slots.map((item: IAreaSlot) => {
      return {
        name: item.name,
        value: item.id,
      };
    });
  }
}
