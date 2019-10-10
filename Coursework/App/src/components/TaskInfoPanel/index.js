import React from 'react'
import {makeStyles} from "@material-ui/core/styles";
import {Drawer, Hidden, TextField, Button,Chip} from "@material-ui/core";
import TaskShow from "./taskShow";

const drawerWidth = 450;

const useStyles = makeStyles(theme => ({
    drawer: {
        [theme.breakpoints.up('md')]: {
            width: drawerWidth,
            flexShrink: 0,
        },
    },
    content: {
        flexGrow: 1,
        padding: theme.spacing(3),
    },
    drawerPaper: {
        width: drawerWidth,
        display: 'flex',
        alignItems: 'center'
    },
    toolbar: theme.mixins.toolbar,
    textField: {
        marginLeft: theme.spacing(1),
        marginRight: theme.spacing(1),
        width: 350,
    },
    button: {
        margin: theme.spacing(1),
    },
    chip: {
        margin: theme.spacing(0.5),
    },
}));

export default function TaskInfoPanel(props) {
    const {selectedTasks} = props.selectedTasks;
        const classes = useStyles();
        const [mobileOpen, setMobileOpen] = React.useState(false);
        function handleDrawerToggle() {
            setMobileOpen(!mobileOpen);
        }

        let content = '';
        if(selectedTasks.length > 1){
            content = <h5>Выбрано: {selectedTasks.length} задач</h5>
        }else if(selectedTasks.length === 1){
            content = <TaskShow task = {selectedTasks[0]}/>
        }

        return(
            <Hidden mdDown implementation='css'>
                <Drawer
                    className={classes.drawer}
                    variant='permanent'
                    anchor='right'
                    onClose={handleDrawerToggle}
                    classes={{
                        paper: classes.drawerPaper,
                    }}
                    ModalProps={{
                        keepMounted: true, // Better open performance on mobile.
                    }}
                >
                    <div className={classes.toolbar} />
                    {content}
                </Drawer>
            </Hidden>
        )
}