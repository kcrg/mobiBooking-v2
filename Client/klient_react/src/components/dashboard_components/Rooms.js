import React, { Component } from 'react'
import '../../styles/dashboard_comp/Rooms.scss';

export default class Rooms extends Component {
  render() {
    return (
      <div className="rooms">
        <h3>Sale</h3>
            <div className="rooms_free">
                <h5>Aktualnie wolne</h5>
                <span>3</span>
            </div>

            <div className="rooms_taken">
                <h5>Aktualnie zajęte</h5>
                <span>0</span>
            </div>
      </div>
    )
  }
}
