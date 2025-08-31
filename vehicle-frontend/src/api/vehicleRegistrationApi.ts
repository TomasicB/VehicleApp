import axios from "axios";
import type {VehicleRegistrationDTO, IVehicleRegistration } from "../types/vehicleRegistration"

const BASE_URL = "http://localhost:5252/api/VehicleRegistration";


export const getVehicleRegistrations = async (params: ListParams) => {
  const response = await axios.get(BASE_URL);
  return response.data;
};

export const createVehicleRegistration = async (data: VehicleRegistrationDTO) => await axios.post(BASE_URL, data);

export const updateVehicleRegistration = async (id: number, data: VehicleRegistrationDTO) => await axios.put(`${BASE_URL}?id=${id}`, data);

export const deleteVehicleRegistration = async (id: number) => await axios.delete(`${BASE_URL}?id=${id}`);