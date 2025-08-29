import { configureStore } from "@reduxjs/toolkit";
import VehicleMakeReducer from "../pages/makes/vehicleMakeSlice";
import VehicleModelReducer from "../pages/models/vehicleModelSlice";
import VehicleOwnerReducer from "../pages/owners/vehicleOwnerSlice";

export const store = configureStore({
  reducer: {
    VehicleMake: VehicleMakeReducer,
    VehicleModel: VehicleModelReducer,
	VehicleOwner: VehicleOwnerReducer
  },
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;
