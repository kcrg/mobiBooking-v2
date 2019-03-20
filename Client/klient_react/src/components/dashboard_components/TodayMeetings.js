import React, { Component } from 'react'
import '../../styles/dashboard_comp/TodayMeetings.scss';
import { library } from '@fortawesome/fontawesome-svg-core';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faMugHot } from '@fortawesome/free-solid-svg-icons';

library.add(faMugHot);

export default class TodayMeetings extends Component {

  state = {
    
  }
  render() {
    return (
      <div className="today_meetings">
        <h3>Spotkania na dziś</h3>
        <div className="time">
            <p>W dniu dzisiejszym nie masz spotkań</p>
        </div>
        <FontAwesomeIcon icon={faMugHot}></FontAwesomeIcon>
      </div>
    )
  }
}
