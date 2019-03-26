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

    state = {
        ip: null
    }

    componentDidMount(){
        const { cookies, ip } = this.props;
        this.setState({
            ip
        })
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
                <div className="boxes">
                    <Meetings ip={this.state.ip}/>
                    <TodayMeetings ip={this.state.ip}/>
                    <ReservButton/>
                    <Rooms ip={this.state.ip}/>
                    <LastReserv ip={this.state.ip}/>
                    <ReservStats ip={this.state.ip}/>
                </div>
            </div>
        )
    }
}
export default withRouter(Dashboard);
