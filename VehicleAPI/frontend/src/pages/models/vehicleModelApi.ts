import axios from 'axios';
import type { VehicleModel, ListParams } from '../../types';

const BASE_URL = 'https://localhost:44311/api/VehicleModel';

export const getVehicleModels = async (params: ListParams) => {
  const response = await axios.get(BASE_URL, { params });
  return response.data; // Expected format: { items: VehicleModel[], total: number }
};

export const createVehicleModel = (data: VehicleModel) => axios.post(BASE_URL, data);
export const updateVehicleModel = (id: number, data: VehicleModel) => axios.put(`${BASE_URL}/${id}`, data);
export const deleteVehicleModel = (id: number) => axios.delete(`${BASE_URL}/${id}`);
