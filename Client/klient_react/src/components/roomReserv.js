import React, { Component } from 'react';
import { withRouter } from 'react-router-dom';
import axios from 'axios';
import Users from './Users';
import '../styles/RoomReserv.scss';

 class RoomReserv extends Component {

  state = {
    reservData: {
      dateFrom: null,
      dateTo: null,
      roomCapacity: null,
      status: 'Wolna',
      title: null,
      invitedUsersIds: [],
      roomId: null
    },
    roomsList: null,
    roomItems: null,
    checked:{
      flipchart: false,
      soundSystem: false,
      repeat: false
    },
    isChecked: false,
    ip: null
  }

  componentWillMount(){
    const { ip } = this.props
    this.setState({
      ip
    })
  }
  componentDidMount(){
    const { cookies } = this.props;
    const { ip } = this.props
    if(cookies.get('token') === undefined){
      this.props.history.push('/');
    }
    axios.get( ip + '/api/Room/get_all')
    .then(res => {
      this.setState({
        roomsList: res.data
      }, this.mapItems)
    })
    .catch(err =>{
      console.log(err)
    })
  }

  mapItems = () =>{
    const roomItems = this.state.roomsList.map(room =>{
    return(
      <option key={room.id} value={room.id}>{room.name}</option>
    )
    })
    this.setState({
      roomItems: roomItems
    })
  }

  handleChange = (name, value) =>{
    this.setState(prevState => ({
      ...prevState,
      reservData: {
        ...prevState.reservData,
        [name]: value
      } 
    }))
  }

  handleCapacityChange = (name, value) =>{
    this.setState(prevState => ({
      ...prevState,
      reservData: {
        ...prevState.reservData,
        [name]: parseInt(value)
      } 
    }))
  }

  handleSubmit = (e) =>{
    e.preventDefault();
  }

  selectChange = (collection) => {
    this.setState(prevState =>({
      ...prevState,
      reservData:{
        ...prevState.reservData,
        roomId: collection
      }
    }), () =>{
      console.log(this.state.reservData.roomId)
    })
  }

  handleCheck = (name, value) =>{
    this.setState(prevState =>({
      ...prevState,
      checked:{
        ...prevState.checked,
        [name]: value
      }
    }), () => {
      console.log(this.state.checked)
    })
  }

  render() {
    return (
        <div className="reserv_div">
          <h2>Zarezerwuj salę:</h2>
          <form onSubmit={this.handleSubmit} className="reserv_form">
            <label htmlFor="dateFrom">Rezerwuję od:</label>
            <input type="text" id="dateFrom" onChange={e => this.handleChange('dateFrom', e.target.value)} required></input>

            <label htmlFor="dateTo">Do:</label>
            <input type="text" id="dateTo" onChange={e => this.handleChange('dateTo', e.target.value)} required></input><br/>
    
            <label htmlFor="roomCapacity" className="other">Pojemność sali:</label>
            <input type="number" id="roomCapacity" onChange={e => this.handleCapacityChange('roomCapacity', e.target.value)} required></input><br/> 

            <div className="eqw">
              <span>Wybierz wyposażenie:</span>
            </div>

            <div className="checkboxes">
              <label htmlFor="flipchart">Flipchart</label>
              <input type="checkbox" value="flipchart" id="flipchart" onChange={e=>{this.handleCheck('flipchart', e.target.checked)}}></input>
           
              <label htmlFor="voice">System nagłaśniający</label>
              <input type="checkbox" value="voice" id="voice" onChange={e=>{this.handleCheck('soundSystem', e.target.checked)}}></input>
            </div>

            <label id="room">Wybierz salę</label>
            <select id="roomTook" onChange={e => {this.selectChange(e.target.value)}}>
              {this.state.roomItems}
            </select>

            <label htmlFor="title">Tytuł spotkania:</label>
            <input type="text" id="title" onChange={e => this.handleChange('title', e.target.value)} required></input><br/>

            <label htmlFor="status">Status:</label>
            <select id="status" onChange={e => this.handleChange('status', e.target.value)}>
              <option>Wolna</option>
              <option>Zajęta</option>
            </select><br/>

            <div className="cyclic">
              <label htmlFor="repeat">Rezerwacja cykliczna:</label>
              <input type="checkbox"  id="repeat" name="repeat" value="repeat" onChange={e=>{this.handleCheck('repeat', e.target.checked)}}></input><br/>
            </div>
            
            <Users ip={this.state.ip}/>
            <input type="submit" value="Rezerwuj"></input>
          </form>
        </div>
    )
  }
}

export default withRouter(RoomReserv);

