import React from 'react'
import {makeStyles} from "@material-ui/core/styles";

import TasksTable from "../TasksTable";

const useStyles = makeStyles(theme => ({
    toolbar: theme.mixins.toolbar,
    content: {
        flexGrow: 1,
        padding: theme.spacing(3),
    },
}));

export default function Content(){
    const classes = useStyles();

    return(
        <React.Fragment>
            <div className={classes.content}>
                <div className={classes.toolbar} />
                <TasksTable/>
            </div>
        </React.Fragment>
    )
}