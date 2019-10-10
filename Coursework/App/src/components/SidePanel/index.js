import React from 'react';
import clsx from 'clsx';
import { makeStyles, useTheme } from '@material-ui/core/styles';
import Drawer from '@material-ui/core/Drawer';
import AppBar from '@material-ui/core/AppBar';
import Toolbar from '@material-ui/core/Toolbar';
import List from '@material-ui/core/List';
import CssBaseline from '@material-ui/core/CssBaseline';
import Typography from '@material-ui/core/Typography';

import ListItem from '@material-ui/core/ListItem';
import ListItemText from '@material-ui/core/ListItemText';
import {ExpansionPanel, ExpansionPanelSummary, ExpansionPanelDetails} from '@material-ui/core';
import ExpandMoreIcon from '@material-ui/icons/ExpandMore';
import {Link} from 'react-router-dom';
import TasksTableContainer from "../../containers/TasksTable";
import TaskInfoContainer from "../../containers/TaskInfo";

const drawerWidth = 240;

const useStyles = makeStyles(theme => ({
    root: {
        display: 'flex',
    },
    appBar: {
        zIndex: theme.zIndex.drawer + 1,
    },
    drawer: {
        width: drawerWidth,
        flexShrink: 0,
    },
    drawerPaper: {
        width: drawerWidth,
    },
    content: {
        flexGrow: 1,
        padding: theme.spacing(3),
    },
    toolbar: theme.mixins.toolbar,
    details:{
        padding: 0
    },
    list:{
        width: '100%'
    },
    logo: {
        textDecoration: 'none',
        color: '#ffffff',
        fontSize: '32px'
    }
}));

export default function SidePanel(props) {
    const {purposes, taskLists} = props;
    const classes = useStyles();

    return (
        <div className={classes.root}>
            <CssBaseline />
            <AppBar position="fixed" className={classes.appBar}>
                <Toolbar>
                    <Typography variant="h6" noWrap>
                        <Link to='/' className={classes.logo}>Logo</Link>
                    </Typography>
                </Toolbar>
            </AppBar>
            <Drawer
                className={classes.drawer}
                variant="permanent"
                classes={{
                    paper: classes.drawerPaper,
                }}
            >
                <div className={classes.toolbar} />
                <List className = {classes.list}>
                    <ListItem button>
                        <ListItemText primary="Сегодня" />
                    </ListItem>
                    <ListItem button>
                        <ListItemText primary="Завтра" />
                    </ListItem>
                    <ListItem button>
                        <ListItemText primary="Эта неделя" />
                    </ListItem>
                    <ListItem button>
                        <ListItemText primary="Следующая неделя" />
                    </ListItem>
                </List>
                <ExpansionPanel>
                    <ExpansionPanelSummary
                        expandIcon={<ExpandMoreIcon />}
                        aria-controls="panel1a-content"
                        id="panel1a-header"
                    >
                        <Typography className={classes.heading}>Списки задач:</Typography>
                    </ExpansionPanelSummary>
                    <ExpansionPanelDetails className = {classes.details}>
                        <List className = {classes.list}>
                            {taskLists.map(list => (
                                <ListItem button key={list.id}>
                                    <ListItemText onClick={() => props.setSelected(list)} primary={list.name} />
                                </ListItem>
                            ))}
                        </List>
                    </ExpansionPanelDetails>
                </ExpansionPanel>
                <ExpansionPanel>
                    <ExpansionPanelSummary
                        expandIcon={<ExpandMoreIcon />}
                        aria-controls="panel1a-content"
                        id="panel1a-header"
                    >
                        <Typography className={classes.heading}>Задачи:</Typography>
                    </ExpansionPanelSummary>
                    <ExpansionPanelDetails className = {classes.details}>
                        <List className = {classes.list}>
                            {purposes.map(prp => (
                                <ListItem button key={prp.id}>
                                    <ListItemText onClick={() => props.setSelected(prp)} primary={prp.name} />
                                </ListItem>
                            ))}
                        </List>
                    </ExpansionPanelDetails>
                </ExpansionPanel>
            </Drawer>
            <main className={classes.content}>
                <TasksTableContainer />
            </main>
            <TaskInfoContainer />
        </div>
    );
}