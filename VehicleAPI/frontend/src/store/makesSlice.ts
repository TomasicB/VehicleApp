import { createSlice, createAsyncThunk} from "@reduxjs/toolkit";
import type { PayloadAction } from "@reduxjs/toolkit";
import { VehicleApi } from "../api/vehicleService";
import type { VehicleMake, PagedResult, ListParams } from "../types/index";

export const getMakes = createAsyncThunk(
  "makes/get",
  async (params: ListParams) => await VehicleApi.listMakes(params)
);

interface State {
  data: PagedResult<VehicleMake> | null;
  loading: boolean;
  error?: string;
  params: ListParams;
}

const initialState: State = {
  data: null,
  loading: false,
  params: { page: 1, pageSize: 10, sort: "name.asc" },
};

const slice = createSlice({
  name: "makes",
  initialState,
  reducers: {
    setParams(state, action: PayloadAction<Partial<ListParams>>) {
      state.params = { ...state.params, ...action.payload };
    },
  },
  extraReducers: (b) => {
    b.addCase(getMakes.pending, (s) => { s.loading = true; s.error = undefined; });
    b.addCase(getMakes.fulfilled, (s, a) => { s.loading = false; s.data = a.payload; });
    b.addCase(getMakes.rejected, (s, a) => { s.loading = false; s.error = a.error.message; });
  },
});

export const { setParams } = slice.actions;
export default slice.reducer;