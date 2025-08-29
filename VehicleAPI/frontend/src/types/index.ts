export interface VehicleMake {
  id: number;
  name: string;
  abrv: string;
}

export interface VehicleModel {
  id: number;
  name: string;
  abrv: string;
  makeId: number;
}

export interface VehicleOwner {
  id: number;
  firstName: string;
  lastName: string;
  DOB: string;
}

export interface VehicleEngine {
  id: number;
  type: string;
  abrv: string
}

export interface VehicleRegistration {
  id: number;
  number: string;
  modelId: number;
  ownerId: number;
  engineId: number;
}

export interface PagedResult<T> {
    items: T[];
    total: number;
    page: number;
    pageSize: number;
}

export interface ListParams {
    page?: number;
    pageSize?: number;
    sort?: string;
    q?: string;
    makeId?: string;
    modelId?: string;
    firstName?: string;
    lastName?: string;
}
