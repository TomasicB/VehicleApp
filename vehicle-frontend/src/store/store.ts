import { configureStore } from "@reduxjs/toolkit";
import VehicleMakeReducer from "./vehicleMakeSlice";
import VehicleModelReducer from "./vehicleModelSlice";
import VehicleOwnerReducer from "./vehicleOwnerSlice";
import VehicleEngineReducer from "./vehicleEngineSlice";
import VehicleRegistrationReducer from "./vehicleRegistrationSlice";

export const store = configureStore({
  reducer: {
    VehicleMakeWriteDTO: VehicleMakeReducer,
    VehicleModelWriteDTO: VehicleModelReducer,
	VehicleOwnerWriteDTO: VehicleOwnerReducer,
    VehicleEngineWriteDTO: VehicleEngineReducer,
	VehicleRegistrationWriteDTO: VehicleRegistrationReducer
  },
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;
