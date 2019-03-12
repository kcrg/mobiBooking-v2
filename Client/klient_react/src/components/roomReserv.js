import React, { Component } from 'react';
import { withRouter } from 'react-router-dom';
import '../css/roomReserv.scss';

 class RoomReserv extends Component {
  render() {
    return (
      <div className="content">
        <div className="form">
            <h2>Zarezerwuj salę:</h2>

            <form>
                <label htmlFor="dateFrom">Rezerwuję od:</label>
                <input type="text" id="dateFrom" onChange={e => this.handleChange('dateFrom', e.target.value)} required></input>

                <label htmlFor="dateTo">Do:</label>
                <input type="text" id="dateTo" onChange={e => this.handleChange('dateTo', e.target.value)} required></input><br/>

                
                <label htmlFor="roomCapacity" className="other">Pojemność sali:</label>
                <input type="number" id="roomCapacity" onChange={e => this.handleChange('roomCapacity', e.target.value)} required></input><br/>  

            </form>
        </div>
      </div>
    )
  }
}

export default withRouter(RoomReserv);

