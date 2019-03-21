import React, { Component } from 'react';
import axios from 'axios';
import { withRouter } from 'react-router-dom';
import '../styles/AddRoom.scss';

class AddRoom extends Component {
  state = {
    roomData:{
      roomName: null,
      location: null,
      availability: 1,
      numberOfPeople: null
    },
    error: 'default',
    succes: 'default',
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
         }, 3000);
      })
    };
  }

  handleSubmit = (e) =>{
    e.preventDefault();
    this.sendData()
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
    const { cookies } = this.props;
    if(cookies.get('token') === undefined){
      this.props.history.push('/');
    }

    axios.get( ip + '/api/Room/get_room_availabilities')
    .then( res=>{
      this.setState(prevState =>({
        ...prevState,
        availability: res.data
      }),  this.mapAva)
    })

  }
  
  render() {
    return (
        <div className="add_room">

          <h2>Dodaj salę:</h2>

          <form onSubmit={this.handleSubmit} className="add_room_form">
            <div className="room_label">
              <label htmlFor="roomName">Nazwa sali:</label>
              <input type="text" id="roomName" onChange={e => this.handleChange('roomName', e.target.value)} required placeholder="Nazwa sali"></input>
            </div>

            <div className="location">
              <label htmlFor="location">Lokalizacja:</label>
              <input type="text" id="location" onChange={e => this.handleChange('location', e.target.value)} required placeholder="Lokalizacja"></input> 
            </div>

            <div className="number_of_people">
              <label htmlFor="numberOfPeople">Liczba osób:</label>
              <input type="number" id="numberOfPeople" onChange={e => this.handleNChange('numberOfPeople', e.target.value)} required></input> 
            </div>

            <div className="availability">
              <label htmlFor="availability">Dostępność:</label>
              <select id="availability" onChange={e => this.handleChange('availability', e.target.value)}>
                {this.state.mapAva}
              </select>
            </div>

            <div className="add_room_submit">
              <input type="submit" value="Zapisz"></input>
            </div>

            <div className={this.state.error}>
              <span>Błąd! Spróbuj ponownie</span>
            </div>

            <div className={this.state.succes}>
              <span>Dodano salę!</span>
            </div>

          </form>
      </div>
    )
  }
}

export default withRouter(AddRoom)
