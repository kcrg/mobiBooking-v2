import React, { Component } from 'react';
import { withRouter } from 'react-router-dom';
import '../css/roomReserv.scss';
import axios from 'axios';
import Users from './Users';

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
      voice: false,
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

  toggleChecked = (name,value) =>{
    this.setState(prevState =>({
      ...prevState,
      checked:{
        ...prevState.checked,
        [name]: value
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

  handleClick = () =>{
    
  }
 
  render() {
    return (
      <div className="content">
        <div className="roomForm">
          <h2>Zarezerwuj salę:</h2>
          <form onSubmit={this.handleSubmit}>
            <label htmlFor="dateFrom">Rezerwuję od:</label>
            <input type="text" id="dateFrom" onChange={e => this.handleChange('dateFrom', e.target.value)} required></input>

            <label htmlFor="dateTo">Do:</label>
            <input type="text" id="dateTo" onChange={e => this.handleChange('dateTo', e.target.value)} required></input><br/>
    
            <label htmlFor="roomCapacity" className="other">Pojemność sali:</label>
            <input type="number" id="roomCapacity" onChange={e => this.handleCapacityChange('roomCapacity', e.target.value)} required></input><br/> 

            <label  id="checking">Potrzebuję z wyposażeniem:</label>
            <input type="checkbox" name="flipchart" onChange={e=>{this.toggleChecked('flipchart', e.target.checked)}}></input><span>Flipchart</span>
            <input type="checkbox" name="voice" onChange={e=>{this.toggleChecked('voice', e.target.checked)}}></input><span>System nagłaśniający</span><br/>

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

            <label htmlFor="repeat">Rezerwacja cykliczna:</label>
            <input type="checkbox"  id="repeat" name="repeat" value="repeat" onChange={e=>{this.toggleChecked('repeat', e.target.checked)}}></input><br/>

            <Users ip={this.state.ip}/>
            <input type="submit" value="Rezerwuj"></input>
          </form>
        </div>
      </div>
    )
  }
}

export default withRouter(RoomReserv);

