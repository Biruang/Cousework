import React, { Component } from 'react';
import { connect } from 'react-redux';
import TasksTable from '../components/TasksTable';
import {setSelectedTasks} from '../actions/selectTasks'

class TasksTableContainer extends Component {
    render() {
        return (
            <TasksTable tasks={this.props.tasks} selected={this.props.selected} setSelected={this.props.setSelectedTasksAct}/>
        );
    }
}

const mapStateToProps = store => {
    console.log(store);
    return{
        tasks: store.tasks,
        selected: store.selected
    }
};

const mapDispatchToProps = dispatch => {
    return {
        setSelectedTasksAct: selectedTasks => dispatch(setSelectedTasks(selectedTasks)),
    }
}

export default connect(mapStateToProps, mapDispatchToProps)(TasksTableContainer);