import { createSlice, createAsyncThunk } from "@reduxjs/toolkit";
import type { IVehicleMake } from "../types/vehicleMake";
import * as api from "../api/vehicleMakeApi";

interface VehicleMakeState {
  items: IVehicleMake[];
  loading: boolean;
}

const initialState: VehicleMakeState = { items: [], loading: false };

export const fetchVehicleMakes = createAsyncThunk("vehicleMakes/get", api.getVehicleMakes);

const vehicleMakeSlice = createSlice({
  name: "vehicleMakes",
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder.addCase(fetchVehicleMakes.pending, (state) => { state.loading = true; });
    builder.addCase(fetchVehicleMakes.fulfilled, (state, action) => {
      state.items = action.payload;
      state.loading = false;
    });
  }
});

export default vehicleMakeSlice.reducer;