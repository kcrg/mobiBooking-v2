import React, { Component } from 'react';
import axios from 'axios';
import { withRouter } from 'react-router-dom';
import '../styles/AddRoom.scss';

class AddRoom extends Component {
  state = {
    roomData:{
      roomName: '',
      location: '',
      availabilityId: 1,
      numberOfPeople: ''
    },
    error: 'default',
    succes: 'default',
    warning: 'default',
    mapAva: null,
    availability:{
      id: null,
      name: null
    }
  }

  handleChange = (name, value) =>{
    this.setState(prevState => ({
      ...prevState,
      roomData: {
        ...prevState.roomData,
        [name]: value
      } 
    }))
  }

  handleNChange = (name, value) =>{
    this.setState(prevState => ({
      ...prevState,
      roomData: {
        ...prevState.roomData,
        [name]: parseInt(value)
      } 
    }))
  }
    
  toggleError = (error) =>{
    if( error === true ){
      this.setState({
        error: 'errors',
        succes: 'default'
      }, () =>{
        setTimeout(() =>{
          this.setState({error: 'default'});
         }, 3000);
      })
    }else{
      this.setState({
        succes: 'done',
        error: 'default'
      }, () =>{
        setTimeout(() =>{
          this.setState({succes: 'default'});
          this.props.history.push('/roomView')
         }, 3000);
      })
    };
  }

  handleSubmit = (e) =>{
    e.preventDefault();
    if(this.checkData()){
      this.setState({
        warning: 'warning'
      }, () =>{
        setTimeout(() =>{
          this.setState({warning: 'default'});
         }, 3000);
      })
    }else
      this.sendData()
  }

  checkData = () =>{
    return (this.state.roomData.roomName.match(/^ *$/) !== null ||
    this.state.roomData.location.match(/^ *$/) !== null||
    this.state.roomData.numberOfPeople === '')
  }


  sendData = () =>{
    const { ip } = this.props;
    axios.post(ip + '/api/Room/create', this.state.roomData)
    .then(res => {
      this.toggleError(false)
      return res;
    })
    .catch(err =>{
      this.toggleError(true)
    });
  }

  mapAva = () =>{
    const Ava = this.state.availability.map(ava =>{
      return(
        <option key={ava.id} value={ava.id}>{ava.name}</option>
      )
    })
    this.setState({
      mapAva: Ava
    })
  }

  componentDidMount(){
    const { ip } = this.props
    axios.get( ip + '/api/Room/get_room_availabilities')
    .then( res=>{
      this.setState(prevState =>({
        ...prevState,
        availability: res.data
      }),  this.mapAva)
    })
  }

  handleButtonClick = () =>{
    this.props.history.push('/roomView')
  }
  
  render() {
    return (
        <div className="add_room">
          <h2>Dodaj salę:</h2>
          <button className="roomList" onClick={this.handleButtonClick}>Lista sal</button>
          <form onSubmit={this.handleSubmit} className="add_room_form">
            <div className="room_label">
              <div className="room">
                <label htmlFor="roomName">Nazwa sali:</label>
              </div>
              <div className="room_input">
                <input type="text" id="roomName" onChange={e => this.handleChange('roomName', e.target.value)} placeholder="Nazwa sali"></input>
              </div>
            </div>

            <div className="location">
              <div className="location_label">
                <label htmlFor="location">Lokalizacja:</label>
              </div>
              <div className="location_input">
                <input type="text" id="location" onChange={e => this.handleChange('location', e.target.value)} placeholder="Lokalizacja"></input> 
              </div>
            </div>

            <div className="number_of_people">
              <div className="number_label">
                <label htmlFor="numberOfPeople">Liczba osób:</label>
              </div>
              <div className="number_input">
                <input type="number" id="numberOfPeople" onChange={e => this.handleNChange('numberOfPeople', e.target.value)}></input>
              </div> 
            </div>

            <div className="availability">
              <div className="ava_label">
                <label htmlFor="availability">Dostępność:</label>
              </div>
              <div className="ava_select">
                <select id="availability" onChange={e => this.handleChange('availabilityId', e.target.value)}>
                  {this.state.mapAva}
                </select>
              </div>
            </div>

            <div className="add_room_submit">
              <input type="submit" value="Zapisz"></input>
            </div>

            <div className={this.state.error}>
              <p>Taka sala już istnieje</p>
            </div>

            <div className={this.state.succes}>
              <p>Dodano salę!</p>
            </div>

            
            <div className={this.state.warning}>
              <p>Uzupełnij wszystkie pola !</p>
            </div>

          </form>
      </div>
    )
  }
}

export default withRouter(AddRoom)
