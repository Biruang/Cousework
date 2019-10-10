import React from "react";
import { Switch, Route, BrowserRouter as Router } from 'react-router-dom';
import AppRouter from '../appRouter';

export default function MainRouter() {
    return(
        <Router>
            <Switch>
                <Route exact path='/' component={AppRouter} />
                <Route path='/login' render = {() => (<h1>Works</h1>)}/>
                <Route render = {() => <h1>Not found</h1>} />;
            </Switch>
        </Router>
    )
}