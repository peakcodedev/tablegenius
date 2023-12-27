export interface ITable {
  id: string;
  tableNumber: number;
  capacity: number;
  description: string;
  areaId: string;
  areaName?: string;
}

export interface ITableWithStatus extends ITable {
  taken: boolean;
}
