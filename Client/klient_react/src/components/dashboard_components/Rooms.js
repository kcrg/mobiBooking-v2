import React, { Component } from 'react'
import '../../styles/dashboard_comp/Rooms.scss';
import axios from 'axios';

export default class Rooms extends Component {

  state = {
    reserveted: null,
    not_reservated: null
  }

  componentWillReceiveProps(nextProps){
    const { ip }  = nextProps
    axios.all([
      axios.get( ip + '/api/Room/get_reservated'),
      axios.get( ip + '/api/Room/get_not_reservated'),
    ])
    .then(axios.spread((reservResponse, notReservResponse) =>{
      console.log('Zarezerwowane:' + reservResponse.data)
      console.log('Wolne:' + notReservResponse.data)
      this.setState({
        reservated: reservResponse.data,
        not_reservated: notReservResponse.data,
      })
    }))
  }

  render() {
    return (
      <div className="rooms">
        <h3>Sale</h3>
        <div className="grid_rooms">
          <div className="rooms_free">
              <h5>Aktualnie wolne</h5>
              <span>{this.state.not_reservated}</span>
          </div>

          <div className="rooms_taken">
              <h5>Aktualnie zajęte</h5>
              <span>{this.state.reservated}</span>
          </div>
          </div>
      </div>
    )
  }
}
