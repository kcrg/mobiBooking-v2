import React, { Component } from 'react';
import { withRouter } from 'react-router-dom';
import axios from 'axios';
import '../styles/RoomReserv.scss';
import Calendar from './Calendar';
import moment from 'moment';
import ReservationIntervals from './ReservationIntervals';
import UserList from './UserList';

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
      dateFrom: moment().format('YYYY-MM-DDTHH:mm'),
      dateTo: moment().format('YYYY-MM-DDTHH:mm'),
      size: 0
    },
    ip: null,
    isVisible: 'none',
    errors: 'default',
    succes: 'default',
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

  render() {
    return (
        <div className="reserv_div">
          <h2>Zarezerwuj salę:</h2>
          <form onSubmit={this.handleSubmit} className="reserv_form">

            <div className="calendar_label">
              <div className="label">
                <label htmlFor="dateFrom">Rezerwuję od:</label>
              </div>
          
              <div className="calendar_input">
                <Calendar onChange={this.handleChange} name = "dateFrom"/>
              </div>
            </div>

            <div className="calendar_label">
              <div className="label_to">
                <label htmlFor="dateTo">Do:</label>
              </div>

              <div className="calendar_input_to">
                <Calendar onChange={this.handleChange} name = "dateTo"/>
              </div>
            </div>

            <div className="room_size_label">
              <div className="room_size">
                <label htmlFor="roomCapacity">Pojemność sali:</label>
              </div>

              <div className="room_number">
                <input type="number" id="roomCapacity" onBlur={e => this.handleCapacityChange('size', e.target.value)} placeholder="Pojemność sali"></input>
              </div>
            </div>

            <div className="select_room">
              <div className="select_label">
                <label id="room">Wybierz salę:</label>
              </div>
              <div className="select_list">
                <select id="roomTook" onChange={e => {this.selectChange(e.target.value)}}>
                  {this.state.roomItems}
                </select>
              </div>
            </div>

            <div className="meeting_title">
              <div className="meeting_label">
                <label htmlFor="title">Tytuł spotkania:</label>
              </div>

              <div className="meeting_input">
                <input type="text" id="title" onChange={e => this.handleChange('title', e.target.value)} placeholder="Tytuł spotkania..."></input>
              </div>
            </div>

            <div className="status">
              <div className="status_label">
                <label htmlFor="status">Status:</label>
              </div>

              <div className="status_select">
                <select id="status" onChange={e => this.handleStatusChange('status', e.target.value)}>
                  <option>Wolna</option>
                  <option>Zajęta</option>
                </select>
              </div>
            </div>

            <div className="cyclic">
              <label htmlFor="repeat">Rezerwacja cykliczna:</label>
              <input type="checkbox"  id="repeat" name="repeat" value="repeat" onChange={e=>{this.handleRepeatCheck('cyclicReservation', e.target.checked)}}></input><br/>
            </div>

            <div className={this.state.isVisible}>
              <ReservationIntervals ip={this.state.ip} onChange={this.handleChange}/>
            </div>

            <UserList  ip={this.state.ip}/>

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

