import React from "react";
import {Route, } from 'react-router-dom';
import App from '../../components/App';
import TasksTableContainer from '../../containers/TasksTable'

export default function AppRouter() {
    return(
        <React.Fragment>
            <Route path="/" component={App} />
        </React.Fragment>
    )
}