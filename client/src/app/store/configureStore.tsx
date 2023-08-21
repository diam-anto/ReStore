//import { createStore } from "redux";
//import counterReducer from "../../features/contact/counterReducer";
import { configureStore } from "@reduxjs/toolkit";
import { counterSlice } from "../../features/contact/counterSlice";
import { TypedUseSelectorHook, useDispatch, useSelector } from "react-redux";
import { basketSlice } from "../../features/basket/basketSlice";
import { catalogSlice } from "../../features/catalog/catalogSlice";

// export function configureStore() {
//     return createStore(counterReducer);
// }

export const store = configureStore({
    reducer: {
        counter: counterSlice.reducer,
        basket: basketSlice.reducer,
        catalog: catalogSlice.reducer
    }
})

// we want just the type of the thing that list returns
export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;

// custom hooks
// instead to use dispatch from react redux , we 're going to use our own custom hook
// which is already typed to AppDispatch , which is of type of store.dispatch
export const useAppDispatch  = () => useDispatch<AppDispatch>();
// calls type selector hook, then we pass in our RootState
export const useAppSelector: TypedUseSelectorHook<RootState> = useSelector;