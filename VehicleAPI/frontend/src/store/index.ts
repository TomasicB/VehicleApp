import { configureStore } from "@reduxjs/toolkit";
import makes from "./makesSlice";
import models from "./modelsSlice";
import owners from "./ownersSlice";

export const store = configureStore({
  reducer: { makes, models, owners },
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;