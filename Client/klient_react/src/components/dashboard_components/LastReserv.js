import React, { Component } from 'react'
import '../../styles/dashboard_comp/LastReserv.scss';
import axios from 'axios';

export default class LastReserv extends Component {

  state = {
    reserv_time: null,
    mappedReserv: null
  }

  componentWillReceiveProps(nextProps){
    const { ip }  = nextProps
    axios.get( ip + '/api/Meetings/last_reservations')
    .then(res =>{
      this.setState({
        reserv_time : res.data
      }, this.mapReserv)
    })
  }

  mapReserv = () =>{
    var i = 0
    if(this.state.reserv_time.length > 0){
    const mappedReserv = this.state.reserv_time.map(reserv =>{
      return(
        <div className="last" key={i++}> 
          <div className="hour">{reserv.date}</div>
          <div>{reserv.title}</div>
          <div>{reserv.roomName}</div>
        </div>
      )
    })
    this.setState({
      mappedReserv
    })}
    else{
      this.setState({
        mappedReserv: 'Jeszcze nie rezerwowałeś!'
      })
    }
  }

  render() {
    return (
      <div className="last_reserv">
        <h3>Ostatnio rezerwowałeś</h3>
        <div className="reserv_time">
            {this.state.mappedReserv}
        </div>
      </div>
    )
  }
}
