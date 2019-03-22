import React, { Component } from 'react';
import axios from 'axios';




 class ReservationIntervals extends Component {

    state = {
        intervals: 1,
        map: null,
        isVisible: 'none'
    }

    handleChange = (id) =>{
        this.props.onChange('reservationIntervalId', id)
    }

    componentDidMount(){
        const { ip } = this.props
        axios.get( ip + '/api/Reservation/get_reservation_intervals')
        .then(res =>{
            this.setState({
                intervals: res.data
            }, this.mapIntervals)
        })
    }

    mapIntervals = () =>{
        const Intervals = this.state.intervals.map(interval =>{
            return(
                <option key={interval.id} value={interval.id}>{interval.name}</option>
            )
        })
        this.setState({
            map: Intervals
        })
    }
    render(){
        return(
            <select onChange={(e) => {this.handleChange(e.target.value)}}>
                {this.state.map}
            </select>
        )
    }
}

export default ReservationIntervals;

