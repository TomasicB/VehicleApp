import { createSlice, createAsyncThunk} from "@reduxjs/toolkit";
import type { PayloadAction } from "@reduxjs/toolkit";
import { VehicleApi } from "../api/vehicleService";
import type { VehicleModel, PagedResult, ListParams } from "../types/index";

export const getModels = createAsyncThunk(
  "models/get",
  async (params: ListParams) => await VehicleApi.listModels(params)
);

interface State {
  data: PagedResult<VehicleModel> | null;
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
  name: "models",
  initialState,
  reducers: {
    setParams(state, action: PayloadAction<Partial<ListParams>>) {
      state.params = { ...state.params, ...action.payload };
    },
  },
  extraReducers: (b) => {
    b.addCase(getModels.pending, (s) => { s.loading = true; s.error = undefined; });
    b.addCase(getModels.fulfilled, (s, a) => { s.loading = false; s.data = a.payload; });
    b.addCase(getModels.rejected, (s, a) => { s.loading = false; s.error = a.error.message; });
  },
});

export const { setParams } = slice.actions;
export default slice.reducer;