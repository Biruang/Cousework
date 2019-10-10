import React, { Component } from 'react';
import { connect } from 'react-redux';
import SidePanel from '../components/SidePanel';
import {setSelected} from "../actions/selected";

class SidePanelContainer extends Component {
    render() {
        return (
            <SidePanel taskLists={this.props.taskLists} purposes={this.props.purposes} setSelected={this.props.setSelected}/>
        );
    }
}

const mapStateToProps = store => {
    console.log(store);
    return{
        taskLists: store.taskLists,
        purposes: store.purposes,
    }
};

const mapDispatchToProps = dispatch => {
    return {
        setSelected: selected => dispatch(setSelected(selected)),
    }
}

export default connect(mapStateToProps, mapDispatchToProps)(SidePanelContainer);