import { get, post, put, del } from "./http";
import type { VehicleMake, VehicleModel, VehicleOwner, PagedResult, ListParams } from "../types/index";

export const VehicleApi = {
  // MAKES
  listMakes: (p?: ListParams, signal?: AbortSignal) =>
    get<PagedResult<VehicleMake>>(`/makes`, p, signal),
  getMake: (id: number, signal?: AbortSignal) =>
    get<VehicleMake>(`/makes/${id}`, undefined, signal),
  createMake: (dto: Pick<VehicleMake, "name">, signal?: AbortSignal) =>
    post<typeof dto, VehicleMake>(`/makes`, dto, signal),
  updateMake: (id: number, dto: Partial<VehicleMake>, signal?: AbortSignal) =>
    put<typeof dto, VehicleMake>(`/makes/${id}`, dto, signal),
  deleteMake: (id: number, signal?: AbortSignal) => del<void>(`/makes/${id}`, signal),

  // MODELS
  listModels: (p?: ListParams, signal?: AbortSignal) =>
    get<PagedResult<VehicleModel>>(`/models`, p, signal),
  getModel: (id: number, signal?: AbortSignal) =>
    get<VehicleModel>(`/models/${id}`, undefined, signal),
  createModel: (dto: Pick<VehicleModel, "name" | "makeId">, signal?: AbortSignal) =>
    post<typeof dto, VehicleModel>(`/models`, dto, signal),
  updateModel: (id: number, dto: Partial<VehicleModel>, signal?: AbortSignal) =>
    put<typeof dto, VehicleModel>(`/models/${id}`, dto, signal),
  deleteModel: (id: number, signal?: AbortSignal) => del<void>(`models/${id}`, signal),

  // OWNERS
  listOwners: (p?: ListParams, signal?: AbortSignal) =>
    get<PagedResult<VehicleOwner>>(`/owners`, p, signal),
  getOwner: (id: number, signal?: AbortSignal) =>
    get<VehicleOwner>(`/owners/${id}`, undefined, signal),
  createOwner: (dto: Pick<VehicleOwner, "firstName" | "lastName" | "DOB" >, signal?: AbortSignal) =>
    post<typeof dto, VehicleOwner>(`/owners`, dto, signal),
  updateOwner: (id: number, dto: Partial<VehicleOwner>, signal?: AbortSignal) =>
    put<typeof dto, VehicleOwner>(`/owners/${id}`, dto, signal),
  deleteOwner: (id: number, signal?: AbortSignal) => del<void>(`/owners/${id}`, signal),
};