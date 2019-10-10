import React from 'react';
import NavPanelMain from "../NavPanelMain";

import './App.css';
import {makeStyles} from "@material-ui/core";
import CssBaseline from "@material-ui/core/CssBaseline";
import SidePanelContainer from "../../containers/SidePanel";
import TaskInfoContainer from "../../containers/TaskInfo";

const useStyles = makeStyles(theme => ({
   root: {
        display: 'flex',
   },
}));

class App extends React.Component {
    constructor(props){
        super(props);
        this.state ={
            classes: useStyles,
        }
    }

    render() {
        const {classes} = this.state;

        return(
            <React.Fragment>
                <CssBaseline/>
                <SidePanelContainer />
                <TaskInfoContainer />
            </React.Fragment>
        );
    }
}

export default App;
