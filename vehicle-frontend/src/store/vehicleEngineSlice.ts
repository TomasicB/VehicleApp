import { createSlice, createAsyncThunk } from "@reduxjs/toolkit";
import type { IVehicleEngine } from "../types/vehicleEngine";
import * as api from "../api/vehicleEngineApi";

interface VehicleEngineState {
  items: IVehicleEngine[];
  loading: boolean;
}

const initialState: VehicleEngineState = { items: [], loading: false };

export const fetchVehicleEngines = createAsyncThunk("vehicleEngines/get", api.getVehicleEngines);

const vehicleEngineSlice = createSlice({
  name: "vehicleEngines",
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder.addCase(fetchVehicleEngines.pending, (state) => { state.loading = true; });
    builder.addCase(fetchVehicleEngines.fulfilled, (state, action) => {
      state.items = action.payload;
      state.loading = false;
    });
  }
});

export default vehicleEngineSlice.reducer;