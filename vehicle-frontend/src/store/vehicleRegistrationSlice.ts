import { createSlice, createAsyncThunk } from "@reduxjs/toolkit";
import type { IVehicleRegistration } from "../types/vehicleRegistration";
import * as api from "../api/vehicleRegistrationApi";

interface VehicleRegistrationState {
  items: IVehicleRegistration[];
  loading: boolean;
}

const initialState: VehicleRegistrationState = { items: [], loading: false };

export const fetchVehicleRegistrations = createAsyncThunk("vehicleRegistrations/get", api.getVehicleRegistrations);

const vehicleRegistrationSlice = createSlice({
  name: "vehicleRegistrations",
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder.addCase(fetchVehicleRegistrations.pending, (state) => { state.loading = true; });
    builder.addCase(fetchVehicleRegistrations.fulfilled, (state, action) => {
      state.items = action.payload;
      state.loading = false;
    });
  }
});

export default vehicleRegistrationSlice.reducer;