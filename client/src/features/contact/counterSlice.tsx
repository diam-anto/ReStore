// piece of state that we create for a feature is a slice of
// our overall redux states
// essential for redux best practices => do not mutate state
// library Emma(is inside redux toolkit) allows us to write mutating update login that becomes correct immutable updates
// if we do something mutable, the library inside is doing this in the background for us to make it immutable and update redux correctly

import { createSlice } from "@reduxjs/toolkit";

export interface CounterState {
    data: number;
    title: string;
}

const initialState: CounterState = {
    data: 42,
    title: 'YARC (yet another redux counter with redux toolkit)'
}


export const counterSlice = createSlice({
    name: 'counter',
    initialState,
    reducers: {
        increment: (state, action) => {
            // that's mutating state 
            // setting state data to its new value
            state.data += action.payload
        },
        decrement: (state, action) => {
            // that's mutating state 
            // setting state data to its new value
            state.data -= action.payload
        },
    }
})

export const {increment, decrement} = counterSlice.actions;