export const SET_TASKS = 'SET_TASK';

export function setTask(tasks) {
    return {
        type: SET_TASKS,
        payload: tasks,
    }
}