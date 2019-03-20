import React, { Component } from 'react';
import { withRouter } from 'react-router-dom';
import '../styles/Dashboard.scss';
import Meetings from './dashboard_components/Meetings';
import TodayMeetings from './dashboard_components/TodayMeetings';
import Rooms from './dashboard_components/Rooms';
import LastReserv from './dashboard_components/LastReserv';
import ReservStats from './dashboard_components/ReservStats';
import ReservButton from './dashboard_components/ReservButton';

class Dashboard extends Component {

    componentDidMount(){
        const { cookies } = this.props;
        if(cookies.get('token') === undefined){
            this.props.history.push('/');
        }
    } 

    buttonClick = () =>{
        this.props.history.push('/roomReserv');
    }

    render() {
        return (
            <div className="dashboard-content">
                <h2>Dashboard</h2>
                <div class="boxes">
                    <Meetings/>
                    <TodayMeetings/>
                    <ReservButton/>
                    <Rooms/>
                    <LastReserv/>
                    <ReservStats/>
                </div>
            </div>
        )
    }
}
export default withRouter(Dashboard);
