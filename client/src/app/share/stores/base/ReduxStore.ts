import { configureStore } from '@reduxjs/toolkit';
import GlobalVarSlice from '../slices/GlobalVarSlice';

export const reduxStore = configureStore({
  reducer: {
    GlobalVar: GlobalVarSlice,
  },
});

export default reduxStore;
export type RootState = ReturnType<typeof reduxStore.getState>;
export type AppDispatch = typeof reduxStore.dispatch;
