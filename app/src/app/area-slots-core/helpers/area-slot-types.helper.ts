import { Injectable } from '@angular/core';

@Injectable()
export class AreaSlotTypesHelper {
  types(): any[] {
    return [
      { name: '--- Typ wählen ---', value: 0 },
      { name: 'Zeitslot (von/bis)', value: 1 },
      { name: 'Zeitslot (Länge in Stunden)', value: 2 },
      { name: 'Einmal pro Tag', value: 3 },
    ];
  }
}
