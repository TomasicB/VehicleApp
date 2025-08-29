import { createSlice, createAsyncThunk } from '@reduxjs/toolkit';
import type { VehicleOwner, ListParams } from '../../types/index';
import { getVehicleOwners } from './vehicleOwnerApi';

interface VehicleOwnerState {
  data: VehicleOwner[];
  loading: boolean;
  total: number;
  status: 'idle' | 'loading' | 'succeeded' | 'failed';
  filters: ListParams;
}

const initialState: VehicleOwnerState = {
  data: [],
  loading: true,
  total: 0,
  status: 'idle',
  filters: JSON.parse(localStorage.getItem('vehicleOwnerFilters') || '{}') || {
    page: 1,
    pageSize: 10,
    sortField: 'LastName',
    sortOrder: 'asc',
    filters: {},
  },
};

export const loadVehicleOwners = createAsyncThunk(
  'vehicleOwner/get',
  async (params: ListParams) => {
    localStorage.setItem('vehicleOwnerFilters', JSON.stringify(params));
    return await getVehicleOwners(params);
  }
);

const vehicleOwnerSlice = createSlice({
  name: 'vehicleOwner',
  initialState,
  reducers: {
    setFilters(state, action) {
      state.filters = { ...state.filters, ...action.payload };
    }
  },
  extraReducers: builder => {
    builder
      .addCase(loadVehicleOwners.pending, state => {
        state.status = 'loading';
      })
      .addCase(loadVehicleOwners.fulfilled, (state, action) => {
        state.status = 'succeeded';
		state.loading = false;
        state.data = action.payload.items;
        state.total = action.payload.total;
      });
  }
});

export const { setFilters } = vehicleOwnerSlice.actions;
export default vehicleOwnerSlice.reducer;
