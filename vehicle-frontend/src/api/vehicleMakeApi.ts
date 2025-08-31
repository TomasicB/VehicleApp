import axios from 'axios';
import type { VehicleMake, ListParams } from '../../types';

const BASE_URL = 'http://localhost:5252/api/VehicleMake';

export const getVehicleMakes = async (params: ListParams) => {
  const response = await axios.get(BASE_URL, { params });
  return response.data;
};

export const createVehicleMake = (data: VehicleMake) => axios.post(BASE_URL, data);
export const updateVehicleMake = (id: number, data: VehicleMake) => axios.put(`${BASE_URL}/${id}`, data);
export const deleteVehicleMake = (id: number) => axios.delete(`${BASE_URL}/${id}`);
