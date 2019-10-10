import {SET_SELECTED} from "../actions/selected";

const initialState = {selected:{ id:0, name:"", color:"" }};

export function selectedReducer(state = initialState, action) {
    switch (action.type) {
        case SET_SELECTED: {
            return {...state, selected: action.payload}
        }
        default:
            return state;
    }
}