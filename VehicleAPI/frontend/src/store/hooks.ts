import { configureStore } from '@reduxjs/toolkit';
import vehicleMakeReducer from '../pages/makes/vehicleMakeSlice';

export const store = configureStore({
  reducer: {
    vehicleMake: vehicleMakeReducer,
    // Add model and owner later
  },
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;
