import { createSlice, createAsyncThunk} from "@reduxjs/toolkit";
import type { PayloadAction } from "@reduxjs/toolkit";
import { VehicleApi } from "../api/vehicleService";
import type { VehicleOwner, PagedResult, ListParams } from "../types/index";

export const getOwners = createAsyncThunk(
  "owners/get",
  async (params: ListParams) => await VehicleApi.listOwners(params)
);

interface State {
  data: PagedResult<VehicleOwner> | null;
  loading: boolean;
  error?: string;
  params: ListParams;
}

const initialState: State = {
  data: null,
  loading: false,
  params: { page: 1, pageSize: 10, sort: "lastName.asc" },
};

const slice = createSlice({
  name: "owners",
  initialState,
  reducers: {
    setParams(state, action: PayloadAction<Partial<ListParams>>) {
      state.params = { ...state.params, ...action.payload };
    },
  },
  extraReducers: (b) => {
    b.addCase(getOwners.pending, (s) => { s.loading = true; s.error = undefined; });
    b.addCase(getOwners.fulfilled, (s, a) => { s.loading = false; s.data = a.payload; });
    b.addCase(getOwners.rejected, (s, a) => { s.loading = false; s.error = a.error.message; });
  },
});

export const { setParams } = slice.actions;
export default slice.reducer;