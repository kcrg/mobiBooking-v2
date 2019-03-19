import React, { Component } from 'react';
import { withRouter } from 'react-router-dom';
import axios from 'axios';
import Users from './Users';
import '../styles/RoomReserv.scss';
import Calendar from './Calendar'



 class RoomReserv extends Component {

  state = {
    reservData: {
      dateFrom: null,
      dateTo: null,
      status: 0,
      title: null,
      invitedUsersIds: [],
      roomId: 1,
      cyclicReservation: false,
      reservationIntervalid: 0
    },
    roomsList: null,
    roomItems: null,
    roomsInfo:{
      flipchart: false,
      soundSystem: false,
      dateFrom: "2019-03-18T14:41",
      dateTo: "2019-03-18T14:42",
      size: 0
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
    if(cookies.get('token') === undefined){
      this.props.history.push('/');
    }
    this.getRooms();
  }

  getRooms = () =>{
    const { ip } = this.props
    axios.post( ip + '/api/Room/for_reservation', this.state.roomsInfo)
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
    if(name === 'dataFrom' || name === 'dataTo'){
      this.setState(prevState =>({
        ...prevState,
        roomsInfo:{
          ...prevState.roomsInfo,
          [name]: value
        }
      }), this.getRooms)
    }else{
      this.setState(prevState => ({
        ...prevState,
        reservData: {
          ...prevState.reservData,
          [name]: value
        } 
      }))
    }
  }

  handleCapacityChange = (name, value) =>{
    this.setState(prevState => ({
      ...prevState,
      roomsInfo: {
        ...prevState.roomsInfo,
        [name]: parseInt(value)
      } 
    }), this.getRooms)
  }

  handleStatusChange = (name, value) =>{
    this.setState(prevState => ({
      ...prevState,
      reservData: {
        ...prevState.reservData,
        [name]: value === 'Wolna' ? (0) : (1)
      } 
    }))
  }


  handleSubmit = (e) =>{
    e.preventDefault();
    console.log(this.state)
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
      roomsInfo:{
        ...prevState.roomsInfo,
        [name]: value
      }
    }), this.getRooms)
  }

  handleRepeatCheck = (name, value) =>{
    this.setState(prevState =>({
      ...prevState,
      reservData:{
        ...prevState.reservData,
        [name]: value
      }
    }), () => {
      console.log(this.state.reservData)
    })
  }

  render() {
    return (
        <div className="reserv_div">
          <h2>Zarezerwuj salę:</h2>
          <form onSubmit={this.handleSubmit} className="reserv_form">
            <label htmlFor="dateFrom">Rezerwuję od:</label>
            <Calendar onChange={this.handleChange}/>

            <label htmlFor="dateTo">Do:</label>
            <Calendar onChange={this.handleChange}/>
    
            <label htmlFor="roomCapacity" className="other" onBlur={this.getRooms()}>Pojemność sali:</label>
            <input type="number" id="roomCapacity" onChange={e => this.handleCapacityChange('size', e.target.value)}></input><br/> 

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
            <select id="status" onChange={e => this.handleStatusChange('status', e.target.value)}>
              <option>Wolna</option>
              <option>Zajęta</option>
            </select><br/>

            <div className="cyclic">
              <label htmlFor="repeat">Rezerwacja cykliczna:</label>
              <input type="checkbox"  id="repeat" name="repeat" value="repeat" onChange={e=>{this.handleRepeatCheck('cyclicReservation', e.target.checked)}}></input><br/>
            </div>
          
            <Users ip={this.state.ip}/>
            <input type="submit" value="Rezerwuj"></input>
          </form>
        </div>
    )
  }
}

export default withRouter(RoomReserv);

