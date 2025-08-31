import axios from "axios";
import type {VehicleEngineDTO, IVehicleEngine } from "../types/vehicleEngine"

const BASE_URL = "http://localhost:5252/api/VehicleEngine";


export const getVehicleEngines = async (params: ListParams) => {
  const response = await axios.get(BASE_URL, { params });
  return response.data;
};

export const createVehicleEngine = async (data: VehicleEngine) => await axios.post(BASE_URL, data);
export const updateVehicleEngine = async (id: number, data: VehicleEngine) => await axios.put(`${BASE_URL}?id=${id}`, data);
export const deleteVehicleEngine = async (id: number) => await axios.delete(`${BASE_URL}?id=${id}`);