import React, { Component } from 'react';
import { withRouter } from 'react-router-dom';
import axios from 'axios';
import Users from './Users';
import '../styles/RoomReserv.scss';
import Calendar from './Calendar';
import moment from 'moment';
import ReservationIntervals from './ReservationIntervals';
import { library } from '@fortawesome/fontawesome-svg-core'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faArrowCircleRight, faArrowCircleLeft } from '@fortawesome/free-solid-svg-icons'

library.add(faArrowCircleRight, faArrowCircleLeft)



 class RoomReserv extends Component {

  state = {
    reservData: {
      dateFrom: moment().format('YYYY-MM-DDTHH:mm'),
      dateTo: moment().format('YYYY-MM-DDTHH:mm'),
      status: 0,
      title: null,
      invitedUsersIds: [],
      roomId: 1,
      cyclicReservation: false,
      reservationIntervalId: 0
    },
    roomsList: null,
    roomItems: null,
    roomsInfo:{
      flipchart: false,
      soundSystem: false,
      dateFrom: moment().format('YYYY-MM-DDTHH:mm'),
      dateTo: moment().format('YYYY-MM-DDTHH:mm'),
      size: 0
    },
    isChecked: false,
    ip: null,
    isVisible: 'none',
    users: null,
    invitedUsers: null,
    errors: 'default',
    succes: 'default'
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
    this.getUsers();
  }

  getUsers = () =>{
    const { ip } = this.props
    axios.get( ip + '/api/Users/get_all')
    .then(res =>{
        this.setState({
           users: res.data 
        })
    })
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
    
      this.setState(prevState =>({
        ...prevState,
        roomsInfo:{
          ...prevState.roomsInfo,
          [name]: value
        }
      }), name !== 'title' ? this.getRooms : null)
    
      this.setState(prevState => ({
            ...prevState,
            reservData: {
              ...prevState.reservData,
              [name]: value
            } 
          }
        )
      )
    
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
    console.log(this.state.reservData)
  }

  selectChange = (collection) => {
    this.setState(prevState =>({
      ...prevState,
      reservData:{
        ...prevState.reservData,
        roomId: collection
      }
    }))
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
    if(value){
      this.setState({
        isVisible: 'isVisible'
      })
    }else{
      this.setState({
        isVisible: 'none'
      })
    }
    this.setState(prevState =>({
      ...prevState,
      reservData:{
        ...prevState.reservData,
        [name]: value
      }
    }))
  }

  arrowRightClick = () =>{
    

  }

  render() {
    return (
        <div className="reserv_div">
          <h2>Zarezerwuj salę:</h2>
          <form onSubmit={this.handleSubmit} className="reserv_form">
            <div className="calendar_label">
              <label htmlFor="dateFrom">Rezerwuję od:</label>
              <Calendar onChange={this.handleChange} name = "dateFrom"/>
            </div>

            <div className="calendar_label">
              <label htmlFor="dateTo">Do:</label>
              <Calendar onChange={this.handleChange} name = "dateTo"/>
            </div>

            <div className="room_size_label">
                <label htmlFor="roomCapacity">Pojemność sali:</label>
                <input type="number" id="roomCapacity" onBlur={e => this.handleCapacityChange('size', e.target.value)} placeholder="Pojemność sali"></input>
            </div>

            <div className="equip">
              <h3>Wybierz wyposażenie:</h3>
            </div>

            <div className="equip_parts">
              <div className="equip_partsv2">
                <label htmlFor="flipchart">Flipchart</label>
                <input type="checkbox" value="flipchart" id="flipchart" onChange={e=>{this.handleCheck('flipchart', e.target.checked)}}></input>
              </div>
            
              <div className="equip_partsv2">
                <label htmlFor="voice">System nagłaśniający</label>
                <input type="checkbox" value="voice" id="voice" onChange={e=>{this.handleCheck('soundSystem', e.target.checked)}}></input>
              </div>
            </div>

            <div className="select_room">
              <label id="room">Wybierz salę:</label>
              <select id="roomTook" onChange={e => {this.selectChange(e.target.value)}}>
                {this.state.roomItems}
              </select>
            </div>

            <div className="meeting_title">
              <label htmlFor="title">Tytuł spotkania:</label>
              <input type="text" id="title" onChange={e => this.handleChange('title', e.target.value)} required placeholder="Tytuł spotkania..."></input>
            </div>

            <div className="status">
              <label htmlFor="status">Status:</label>
              <select id="status" onChange={e => this.handleStatusChange('status', e.target.value)}>
                <option>Wolna</option>
                <option>Zajęta</option>
              </select>
            </div>

            <div className="cyclic">
              <label htmlFor="repeat">Rezerwacja cykliczna:</label>
              <input type="checkbox"  id="repeat" name="repeat" value="repeat" onChange={e=>{this.handleRepeatCheck('cyclicReservation', e.target.checked)}}></input><br/>
            </div>

            <div className={this.state.isVisible}>
              <ReservationIntervals ip={this.state.ip} onChange={this.handleChange}/>
            </div>


            <div className="users_div">
              <Users users={this.state.users}/>
              <div className="arrows">
                <FontAwesomeIcon icon={faArrowCircleLeft} onClick = {this.arrowLeftClick}></FontAwesomeIcon>
                <FontAwesomeIcon icon={faArrowCircleRight} onClick = {this.arrowRightClick}></FontAwesomeIcon>
              </div>
              <Users users={this.state.invitedUsers}/>
            </div>

            <div className="reserv_submit">
              <input type="submit" value="Rezerwuj"></input>
            </div>
          </form>

          <div className={this.state.error}>
          <p>Istnieje użytkownik o podanej nazwie użytkownika lub adresie e-mail!</p>
        </div>

        <div className={this.state.succes}>
          <p>Pomyślnie dodano użytkownika!</p>
        </div>

        </div>
    )
  }
}

export default withRouter(RoomReserv);

