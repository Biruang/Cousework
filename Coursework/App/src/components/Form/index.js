import React from 'react';
import Button from '@material-ui/core/Button';
import TextField from '@material-ui/core/TextField';
import Dialog from '@material-ui/core/Dialog';
import DialogActions from '@material-ui/core/DialogActions';
import DialogContent from '@material-ui/core/DialogContent';
import DialogContentText from '@material-ui/core/DialogContentText';
import DialogTitle from '@material-ui/core/DialogTitle';
import TaskInfoPanel from "../TaskInfoPanel";
import {Chip, makeStyles} from "@material-ui/core";

const useStyles = makeStyles(theme => ({
    content: {
        flexGrow: 1,
        padding: theme.spacing(3),
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

export default function FormDialog() {
    const classes = useStyles()

    const [open, setOpen] = React.useState(false);

    const handleClickOpen = () => {
        setOpen(true);
    };

    const handleClose = () => {
        setOpen(false);
    };

    return (
        <div>
            <Button variant="outlined" color="primary" onClick={handleClickOpen}>
                Create new task
            </Button>
            <Dialog open={open} onClose={handleClose} aria-labelledby="form-dialog-title">
                <DialogTitle id="form-dialog-title">Create new task</DialogTitle>
                <DialogContent>
                    <React.Fragment>
                        <TextField
                            id="standard-with-placeholder"
                            label='name'
                            placeholder='name'
                            className={classes.textField}
                            helperText="Name"
                            margin="normal"
                        />
                        <TextField
                            id="standard-with-placeholder"
                            placeholder='description'
                            className={classes.textField}
                            multiline
                            helperText="Description"
                            margin="normal"
                        />
                        <TextField
                            id="standard-with-placeholder"
                            label='creationTime'
                            placeholder='creationTime'
                            className={classes.textField}
                            helperText="Name"
                            margin="normal"
                        />
                    </React.Fragment>
                </DialogContent>
                <DialogActions>
                    <Button onClick={handleClose} color="primary">
                        Save
                    </Button>
                    <Button onClick={handleClose} color="primary">
                        Cancel
                    </Button>
                </DialogActions>
            </Dialog>
        </div>
    );
}