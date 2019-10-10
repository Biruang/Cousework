import React from 'react'
import {makeStyles} from "@material-ui/core/styles";
import {Drawer, Hidden, TextField, Button,Chip} from "@material-ui/core";

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

export default function TaskShow(props) {
    const classes = useStyles();
    const [chipData3, setChipData3] = React.useState(props.task.taskLists);
    const [chipData2, setChipData2] = React.useState(props.task.reminders);
    const [chipData1, setChipData1] = React.useState(props.task.purpouse);
    const handleDelete2 = chipToDelete => () => {
        setChipData2(chips => chips.filter(chip => chip.key !== chipToDelete.key));
    };
    const handleDelete3 = chipToDelete => () => {
        setChipData3(chips => chips.filter(chip => chip.key !== chipToDelete.key));
    };
    let {name, description, creationTime} = props.task;

    return(
        <React.Fragment>
            <TextField
                id="standard-with-placeholder"
                label={name}
                placeholder={name}
                className={classes.textField}
                helperText="Name"
                margin="normal"
            />
            <TextField
                id="standard-with-placeholder"
                placeholder={description}
                className={classes.textField}
                multiline
                helperText="Description"
                margin="normal"
            />
            <TextField
                id="standard-with-placeholder"
                label={creationTime}
                placeholder={creationTime}
                className={classes.textField}
                helperText="Creation time"
                margin="normal"
            />
            <h5>Purpose:</h5>

            <h5>Reminders:</h5>
            {chipData2.map(list => {
                return (
                    <Chip
                        key={list.id}
                        label={list.name}
                        onDelete={handleDelete2(list)}
                        className={classes.chip}
                    />)
            })}
            <h5>Task lists:</h5>
            {chipData3.map(list => {
                return (
                    <Chip
                        key={list.id}
                        label={list.name}
                        onDelete={handleDelete3(list)}
                        className={classes.chip}
                    />)
            })}
            <div>
                <Button variant="contained" color="primary" className={classes.button}>
                    Save
                </Button>
                <Button className={classes.button}>Cancel</Button>
            </div>
        </React.Fragment>
    )
}
