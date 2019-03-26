import React, { Component } from 'react'
import '../../styles/dashboard_comp/TodayMeetings.scss';
import { library } from '@fortawesome/fontawesome-svg-core';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faMugHot } from '@fortawesome/free-solid-svg-icons';
import axios from 'axios';

library.add(faMugHot);

export default class TodayMeetings extends Component {

  state = {
    today_meetings: null,
    mapped_today: null
  }

  componentWillReceiveProps(nextProps){
    const { ip } = nextProps
    axios.get( ip + '/api/Meetings/today')
    .then(res =>{
      this.setState({
        today_meetings: res.data
      }, this.mapTodayMeetings)
    })
  }

  mapTodayMeetings = () =>{
    if(this.state.today_meetings.length > 0){
      var i = 0;
      const meeting = this.state.today_meetings.map(meeting =>{
      return(
        <div className="mapped_today" key={i++}>
          <div className="hour">{meeting.time}</div>
          <div>{meeting.title}</div>
          <div>{meeting.roomName}</div>
        </div>
      )
    })
    this.setState({
      mapped_today: meeting
    })}
    else{
      this.setState({
        mapped_today: 'W dniu dzisiejszym nie masz spotkań'
      })
    }
  }
  render() {
    return (
      <div className="today_meetings">
        <h3>Spotkania na dziś</h3>
        <div className="time">
            {this.state.mapped_today}
        </div>
        <FontAwesomeIcon icon={faMugHot}></FontAwesomeIcon>
      </div>
    )
  }
}
