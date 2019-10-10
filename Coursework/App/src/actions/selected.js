export const SET_SELECTED = 'SET_SELECTED';

export function setSelected(selected) {
    return {
        type: SET_SELECTED,
        payload: selected,
    }
}