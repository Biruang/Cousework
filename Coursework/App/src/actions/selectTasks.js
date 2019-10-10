export const SET_SELECTED_TASKS = 'SET_SELECTED_TASKS';

export function setSelectedTasks(selectedTasks) {
    return {
        type: SET_SELECTED_TASKS,
        payload: selectedTasks,
    }
}