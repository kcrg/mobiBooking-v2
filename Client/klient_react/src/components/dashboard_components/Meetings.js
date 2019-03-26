import React, { Component } from 'react';
import '../../styles/dashboard_comp/Meetings.scss';
import { library } from '@fortawesome/fontawesome-svg-core';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faClock } from '@fortawesome/free-solid-svg-icons';
import axios from 'axios';

library.add(faClock)

export default class Meetings extends Component {

    state = {
      this_week: null,
      last_week: null,
      this_month: null,
      last_month: null
    }

      componentWillReceiveProps(nextProps){
        const { ip }  = nextProps
        axios.all([
          axios.get( ip + '/api/Meetings/this_week'),
          axios.get( ip + '/api/Meetings/last_week'),
          axios.get( ip + '/api/Meetings/this_month'),
          axios.get( ip + '/api/Meetings/last_month')
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
      <div className="meetings">
        <div className="logos">
          <h3>Moje spotkania</h3>
          <FontAwesomeIcon icon={faClock}></FontAwesomeIcon>
        </div>
        
        <div className="whole">
          <div className="this_week">
              <h5>ten tydzień</h5>
              <span>{this.state.this_week}h</span>
          </div>

          <div className="previous_week">
            <h5>poprz. tydzień</h5>
            <span>{this.state.last_week}h</span>
          </div>

          <div className="this_month">
            <h5>ten miesiąc</h5>
            <span>{this.state.this_month}h</span>
          </div>

          <div className="previous_month">
              <h5>poprz. miesiąc</h5>
              <span>{this.state.last_month}h</span>
          </div>
        </div>
      </div>
    )
  }
}
