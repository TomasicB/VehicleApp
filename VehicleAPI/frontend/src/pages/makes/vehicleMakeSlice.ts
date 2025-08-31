import { createSlice, createAsyncThunk } from '@reduxjs/toolkit';
import type { VehicleMake, ListParams } from '../../types/index';
import { getVehicleMakes } from './vehicleMakeApi';

interface VehicleMakeState {
  data: VehicleMake[];
  loading: boolean;
  total: number;
  status: 'idle' | 'loading' | 'succeeded' | 'failed';
  filters: ListParams;
}

const initialState: VehicleMakeState = {
  data: [],
  loading: true,
  total: 0,
  status: 'idle',
  filters: JSON.parse(localStorage.getItem('vehicleMakeFilters') || '{}') || {
    page: 1,
    pageSize: 10,
    sortField: 'name',
    sortOrder: 'asc',
    filters: {},
  },
};

export const loadVehicleMakes = createAsyncThunk(
  'vehicleMake/get',
  async (params: ListParams) => {
    localStorage.setItem('vehicleMakeFilters', JSON.stringify(params));
    return await getVehicleMakes(params);
  }
);

const vehicleMakeSlice = createSlice({
  name: 'vehicleMake',
  initialState,
  reducers: {
    setFilters(state, action) {
      state.filters = { ...state.filters, ...action.payload };
    }
  },
  extraReducers: builder => {
    builder
      .addCase(loadVehicleMakes.pending, state => {
        state.status = 'loading';
      })
      .addCase(loadVehicleMakes.fulfilled, (state, action) => {
        state.status = 'succeeded';
        state.data = action.payload.items;
        state.total = action.payload.total;
      });
    }
});

export const { setFilters } = vehicleMakeSlice.actions;
export default vehicleMakeSlice.reducer;
