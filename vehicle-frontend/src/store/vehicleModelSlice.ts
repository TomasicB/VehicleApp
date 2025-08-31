import { createSlice, createAsyncThunk } from "@reduxjs/toolkit";
import type { IVehicleModel } from "../types/vehicleModel";
import * as api from "../api/vehicleModelApi";

interface VehicleModelState {
  items: IVehicleModel[];
  loading: boolean;
}

const initialState: VehicleModelState = { items: [], loading: false };

export const fetchVehicleModels = createAsyncThunk("vehicleModels/get", api.getVehicleModels);

const vehicleModelSlice = createSlice({
  name: "vehicleModels",
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder.addCase(fetchVehicleModels.pending, (state) => { state.loading = true; });
    builder.addCase(fetchVehicleModels.fulfilled, (state, action) => {
      state.items = action.payload;
      state.loading = false;
    });
  }
});

export default vehicleModelSlice.reducer;