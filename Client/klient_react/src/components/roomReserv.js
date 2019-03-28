import React, { Component } from 'react';
import { withRouter } from 'react-router-dom';
import axios from 'axios';
import '../styles/RoomReserv.scss';
import Calendar from './Calendar';
import moment from 'moment';
import ReservationIntervals from './ReservationIntervals';
import UserList from './UserList';
import { connect } from 'react-redux';

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
      reservationIntervalId: null
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
    warning: 'default',
    disabled: true,
    times: 'times'
  }

  componentWillMount(){
    const { ip } = this.props
    this.setState({
      ip
    })
  }
  componentDidMount(){
    if(this.props.data === true){
      axios.get(this.props.ip + '/api/Room/get/' + this.props.id)
      .then(res =>{
        console.log(res.data)
      })
      .catch(err =>{

      })
    }else{
      this.getRooms();
    }
    
  }

  getRooms = () =>{
    this.setState({
      disabled: true,
      roomsList: []
    }, this.mapItems)
    const { ip } = this.props
    axios.post( ip + '/api/Room/for_reservation', this.state.roomsInfo)
    .then(res => {
      if(res.data.length > 0){
        this.setState(prevState =>({
          ...prevState,
          roomsList: res.data,
          disabled: false,
          times: 'default',
          reservData: {
          ...prevState.reservData,
          roomId:   res.data[0].id
         }
        }), ()=>{
          this.mapItems()
          console.log(this.state.roomsList)
        })
      }else{
        this.setState({
          times: 'times'
        })
      }
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
        [name]: parseInt(value === '' ? ('0') : (value))
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
    if(this.state.reservData.invitedUsersIds.length === 0 
      || this.state.reservData.title === null
      || (this.state.reservData.title.match(/^ *$/) !== null)){
      this.setState({
        warning: 'warning'
      }, () =>{
        setTimeout(() =>{
          this.setState({warning: 'default'});
         }, 3000);
      })
    }else
    this.sendData();
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

  UpdateInvitedUsers = (invitedUsersIds) =>{
    this.setState(prevState =>({
      ...prevState,
      reservData:{
        ...prevState.reservData,
        invitedUsersIds: invitedUsersIds
      }
    }))
  }

  sendData = () =>{
    axios.post( this.state.ip + '/api/Reservation/create', this.state.reservData)
    .then(res =>{
      this.toggleError(false)
      this.getRooms();
    })
    .catch(err =>{
      this.toggleError(true)
    })
  }

  toggleError = (error) =>{
    if( error === true ){
      this.setState({
        errors: 'wrong'
      })
      setTimeout(() =>{
        this.setState({errors: 'default'});
       }, 3000);
    }
    else{
      this.setState({
        succes: 'done'
      })
        setTimeout(() =>{
        this.setState({succes: 'default'});
        this.props.history.push('/home')
       }, 3000);
    };
  }

  render() {
    console.log(this.props.data)
    return (
        <div className="reserv_div">
          <h2>Zarezerwuj salę:</h2>
          <form onSubmit={this.handleSubmit} className="reserv_form">

            <div className="calendar_label">
              <div className="label">
                <label htmlFor="dateFrom">Rezerwuję od: <span className="star">*</span></label>
              </div>
          
              <div className="calendar_input">
                <Calendar onChange={this.handleChange} name = "dateFrom"/>
              </div>
            </div>

            <div className="calendar_label">
              <div className="label_to">
                <label htmlFor="dateTo">Rezerwuję do: <span className="star">*</span></label>
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
                <select id="roomTook" onChange={e => {this.selectChange(e.target.value)}} disabled={this.state.disabled}>
                  {this.state.roomItems}
                </select>
              </div>
            </div>

            <div className="meeting_title">
              <div className="meeting_label">
                <label htmlFor="title">Tytuł spotkania: <span className="star">*</span></label>
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
            
            <UserList  ip={this.state.ip} updateInvitedUsers={this.UpdateInvitedUsers}/>

            <div className="reserv_submit">
              <input type="submit" value="Rezerwuj" disabled={this.state.disabled}></input>
            </div>
          </form>

          <div className={this.state.errors}>
          <p>Błąd przy dodawaniu rezerwacji</p>
        </div>

        <div className={this.state.succes}>
          <p>Pomyślnie dodano rezerwację!</p>
        </div>

        <div className={this.state.warning}>
          <p>Uzupełnij wszystkie pola!</p>
        </div>

        <div className={this.state.times}>
          <p>Brak dostępnych sal w danym przedziale czasowym lub z określoną pojemnością!</p>
        </div>

        </div>
    )
  }
}

const MapStateToProps = (state) =>({
  id: state.id,
  data: state.data
})

export default connect(MapStateToProps)(withRouter(RoomReserv));

