import {SET_SELECTED_TASKS} from '../actions/selectTasks'

const initialState = {
    selectedTasks: []
};

export function selectedTasksReducer(state = initialState, action) {
    switch (action.type) {
        case SET_SELECTED_TASKS: {
            return {...state, selectedTasks: action.payload}
        }
        default:
            return state;
    }
}