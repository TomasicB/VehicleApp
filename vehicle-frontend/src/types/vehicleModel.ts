export interface VehicleModelDTO {
  name: string | null;
  vehicleMake: VehicleMakeDTO;
  abrv?: string | null;
  
  vehicleRegistrations?: VehicleRegistrationDTO[] | null;
}

export interface VehicleModelWriteDTO {
  id?: number;
  name: string | null;
  abrv?: string | null;
  
  vehicleMake: VehicleMakeWriteDTO;
  vehicleRegistrations?: VehicleRegistrationDTO[] | null;
}

export interface IVehicleModel {
  name?: string | null;
  abrv?: string | null;
  vehicleMake: VehicleMakeDTO;
  vehicleRegistrations?: VehicleRegistrationDTO[] | null;
}

export interface VehicleMakeDTO { name: string | null; abrv?: string | null; vehicleModels?: VehicleModelDTO[] | null; }
export interface VehicleMakeWriteDTO { id?: number; name: string | null; abrv?: string | null; vehicleModels?: VehicleModelDTO[] | null; }
export interface VehicleRegistrationDTO { registrationNumber: string | null; vehicleOwner: VehicleOwnerDTO; vehicleModel: VehicleModelDTO; vehicleEngine: VehicleEngineDTO;}
export interface VehicleOwnerDTO { firstName: string | null; lastName: string | null; dob?: string; vehicleRegistrations?: VehicleRegistrationDTO[] | null; }
export interface VehicleEngineDTO { type: string | null; abrv?: string | null; vehicleRegistrations?: VehicleRegistrationDTO[] | null; }
