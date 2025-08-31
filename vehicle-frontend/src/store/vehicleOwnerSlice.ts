import { createSlice, createAsyncThunk } from "@reduxjs/toolkit";
import type { IVehicleOwner } from "../types/vehicleOwner";
import * as api from "../api/vehicleOwnerApi";

interface VehicleOwnerState {
  items: IVehicleOwner[];
  loading: boolean;
}

const initialState: VehicleOwnerState = { items: [], loading: false };

export const fetchVehicleOwners = createAsyncThunk("vehicleOwners/get", api.getVehicleOwners);

const vehicleOwnerSlice = createSlice({
  name: "vehicleOwners",
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder.addCase(fetchVehicleOwners.pending, (state) => { state.loading = true; });
    builder.addCase(fetchVehicleOwners.fulfilled, (state, action) => {
      state.items = action.payload;
      state.loading = false;
    });
  }
});

export default vehicleOwnerSlice.reducer;