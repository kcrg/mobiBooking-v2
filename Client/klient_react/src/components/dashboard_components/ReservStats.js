import React, { Component } from 'react'
import '../../styles/dashboard_comp/ReservStats.scss';
import { library } from '@fortawesome/fontawesome-svg-core';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faCalendar } from '@fortawesome/free-solid-svg-icons';
import axios from 'axios';

library.add(faCalendar);

export default class ReservStats extends Component {

  state = {
    this_week: null,
    last_week: null,
    this_month: null,
    last_month: null
  }

  componentWillReceiveProps(nextProps){
    const { ip }  = nextProps
    axios.all([
      axios.get( ip + '/api/Meetings/count_this_week'),
      axios.get( ip + '/api/Meetings/count_last_week'),
      axios.get( ip + '/api/Meetings/count_this_month'),
      axios.get( ip + '/api/Meetings/count_last_month')
    ])
    .then(axios.spread((thisResponse, lastResponse, monthResponse, lastMonthResponse ) =>{
      this.setState({
        this_week: thisResponse.data,
        last_week: lastResponse.data,
        this_month: monthResponse.data,
        last_month: lastMonthResponse.data
      })
    }))
  }

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
              <span>{this.state.this_week}</span>
          </div>
          
          <div className="reserv_previous_week">
              <h5>poprz. tydzień</h5>
              <span>{this.state.last_week}</span>
          </div>

          <div className="reserv_this_month">
            <h5>ten miesiąc</h5>
            <span>{this.state.this_month}</span>
          </div>

          <div className="reserv_previous_month">
              <h5>poprz. miesiąc</h5>
              <span>{this.state.last_month}</span>
          </div>
        </div>
      </div>
    )
  }
}
