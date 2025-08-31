import { configureStore } from "@reduxjs/toolkit";
import vehicleMakeReducer from "./vehicleMakeSlice";

export const store = configureStore({
  reducer: {
    vehicleMakes: vehicleMakeReducer,
    // add other slices
  },
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;
