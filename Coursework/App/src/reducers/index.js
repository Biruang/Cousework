import {combineReducers} from "redux";
import {tasksReducer} from './tasks';
import {taskListsReducer} from './taskLists';
import {remindersReducer} from './reminders';
import {purposesReducer} from './purposes';
import {userReducer} from "./user";
import {selectedTasksReducer} from "./selectedTasks"
import {selectedReducer} from "./selected";

export const rootReducer = combineReducers({
    tasks: tasksReducer,
    taskLists: taskListsReducer,
    reminder: remindersReducer,
    purposes: purposesReducer,
    user: userReducer,
    selectedTasks: selectedTasksReducer,
    selected: selectedReducer,
});