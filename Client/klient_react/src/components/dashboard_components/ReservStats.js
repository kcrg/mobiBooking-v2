import React, { Component } from 'react'
import '../../styles/dashboard_comp/ReservStats.scss';
import { library } from '@fortawesome/fontawesome-svg-core';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faCalendar } from '@fortawesome/free-solid-svg-icons';

library.add(faCalendar);

export default class ReservStats extends Component {
  render() {
    return (
      <div className="reserv_stats">
        <div className="res_logo">
          <h3>Statystyki rezerwacji</h3>
          <FontAwesomeIcon icon={faCalendar}></FontAwesomeIcon>
        </div>

        <div className="res_whole">
          <div className="reserv_this_week">
              <h5>ten tydzień</h5>
              <span>0.00h</span>
          </div>
          
          <div className="reserv_previous_week">
              <h5>poprz. tydzień</h5>
              <span>0.00h</span>
          </div>

          <div className="reserv_this_month">
            <h5>ten miesiąc</h5>
            <span>0.00h</span>
          </div>

          <div className="reserv_previous_month">
              <h5>poprz. miesiąc</h5>
              <span>0.00h</span>
          </div>
        </div>
      </div>
    )
  }
}
