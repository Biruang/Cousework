import React from 'react'
import AppBar from "@material-ui/core/AppBar";
import Toolbar from "@material-ui/core/Toolbar";
import IconButton from "@material-ui/core/IconButton";
import MenuIcon from '@material-ui/icons/Menu';

import {makeStyles} from "@material-ui/core/styles";
import {Typography} from "@material-ui/core";


const useStyles = makeStyles(theme => ({
    appBar: {
        zIndex: theme.zIndex.drawer + 1,
    },
    menuButton: {
        marginRight: theme.spacing(2),
        [theme.breakpoints.up('sm')]: {
            display: 'none',
        },
    },
    title: {
        flexGrow: 1,
    },
}));

export default function NavPanelMain(){
    const classes = useStyles();

    return(
        <React.Fragment>
        <AppBar position='fixed' className={classes.appBar}>
            <Toolbar>
                <IconButton edge="start" className={classes.menuButton} color="inherit" aria-label="menu">
                    <MenuIcon />
                </IconButton>
                <Typography variant='h4' className={classes.title}>
                    Logo
                </Typography>
            </Toolbar>
        </AppBar>
        </React.Fragment>
    )
}