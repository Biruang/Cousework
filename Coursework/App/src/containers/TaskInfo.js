import React, { Component } from 'react';
import { connect } from 'react-redux';
import TaskInfo from '../components/TaskInfoPanel';

class TaskInfoContainer extends Component {
    render() {
        return (
            <TaskInfo selectedTasks={this.props.selectedTasks}/>
        );
    }
}

const mapStateToProps = store => {
    console.log(store);

    return{
        selectedTasks: store.selectedTasks,
    }
};

export default connect(mapStateToProps)(TaskInfoContainer);