import React, { Component } from 'react'
import '../../styles/dashboard_comp/LastReserv.scss';

export default class LastReserv extends Component {
  render() {
    return (
      <div className="last_reserv">
        <h3>Ostatnio rezerwowałeś</h3>

        <div className="reserv_time">
            <span>Jeszcze nie rezerwowałeś</span>
        </div>

      </div>
    )
  }
}
