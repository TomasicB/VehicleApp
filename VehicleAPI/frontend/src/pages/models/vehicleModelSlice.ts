import { createSlice, createAsyncThunk } from '@reduxjs/toolkit';
import type { VehicleModel, ListParams } from '../../types/index';
import { getVehicleModels } from './vehicleModelApi';

interface VehicleModelState {
  data: VehicleModel[];
  loading: boolean;
  total: number;
  status: 'idle' | 'loading' | 'succeeded' | 'failed';
  filters: ListParams;
}

const initialState: VehicleModelState = {
  data: [],
  loading: true,
  total: 0,
  status: 'idle',
  filters: JSON.parse(localStorage.getItem('vehicleModelFilters') || '{}') || {
    page: 1,
    pageSize: 10,
    sortField: 'name',
    sortOrder: 'asc',
    filters: {},
  },
};

export const loadVehicleModels = createAsyncThunk(
  'vehicleModel/get',
  async (params: ListParams) => {
    localStorage.setItem('vehicleModelFilters', JSON.stringify(params));
    return await getVehicleModels(params);
  }
);

const vehicleModelSlice = createSlice({
  name: 'vehicleModel',
  initialState,
  reducers: {
    setFilters(state, action) {
      state.filters = { ...state.filters, ...action.payload };
    }
  },
  extraReducers: builder => {
    builder
      .addCase(loadVehicleModels.pending, state => {
        state.status = 'loading';
      })
      .addCase(loadVehicleModels.fulfilled, (state, action) => {
        state.status = 'succeeded';
        state.data = action.payload.items;
        state.total = action.payload.total;
      });
  }
});

export const { setFilters } = vehicleModelSlice.actions;
export default vehicleModelSlice.reducer;
