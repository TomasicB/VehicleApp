import axios from 'axios';
import type { VehicleOwner, ListParams } from '../../types';

const BASE_URL = 'http://localhost:5252/api/VehicleOwner';

export const getVehicleOwners = async (params: ListParams) => {
  const response = await axios.get(BASE_URL, { params });
  return response.data;
};

export const createVehicleOwner = (data: VehicleOwner) => axios.post(BASE_URL, data);
export const updateVehicleOwner = (id: number, data: VehicleOwner) => axios.put(`${BASE_URL}/${id}`, data);
export const deleteVehicleOwner = (id: number) => axios.delete(`${BASE_URL}/${id}`);
