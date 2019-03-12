import React, { Component } from 'react';
import { withRouter } from 'react-router-dom';
import '../css/roomReserv.scss';

 class RoomReserv extends Component {

    state = {
        reservData: {
            dateFrom: null,
            dateTo: null,
            roomCapacity: null,
            status: null,
            title: null,
            invitedUsersIds: []
        }
    }

    componentDidMount(){
        const { cookies } = this.props;
        if(cookies.get('token') === undefined){
          this.props.history.push('/');
        } 
      }

      
  render() {
    return (
      <div className="content">
        <div className="roomForm">
            <h2>Zarezerwuj salę:</h2>

            <form>
                <label htmlFor="dateFrom">Rezerwuję od:</label>
                <input type="text" id="dateFrom" onChange={e => this.handleChange('dateFrom', e.target.value)} required></input>

                <label htmlFor="dateTo">Do:</label>
                <input type="text" id="dateTo" onChange={e => this.handleChange('dateTo', e.target.value)} required></input><br/>

                
                <label htmlFor="roomCapacity" className="other">Pojemność sali:</label>
                <input type="number" id="roomCapacity" onChange={e => this.handleChange('roomCapacity', e.target.value)} required></input><br/> 

                <label  id="checking">Potrzebuję z wyposażeniem:</label>
                <input type="checkbox" name="flipchart" value="flipchart"></input><span>Flipchart</span>
                <input type="checkbox" name="voice" value="voice"></input><span>System nagłaśniający</span><br/>

                <label id="room">Wybierz salę</label>
                <select id="roomTook">
                    <option>-wybierz-</option>
                </select>

                <label htmlFor="title">Tytuł spotkania:</label>
                <input type="text" id="title" onChange={e => this.handleChange('title', e.target.value)} required></input><br/>

                <label htmlFor="status">Status:</label>
                <select id="status">
                    <option>Wolna</option>
                    <option>Zajęta</option>
                </select><br/>

                <label htmlFor="repeat">Rezerwacja cykliczna:</label>
                <input type="checkbox"  id="repeat" name="repeat" value="repeat"></input>
            </form>
        </div>
      </div>
    )
  }
}

export default withRouter(RoomReserv);

