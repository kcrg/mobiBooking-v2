import React, { Component } from 'react';
import axios from 'axios';
import { withRouter } from 'react-router-dom';
import '../styles/AddRoom.scss';

class AddRoom extends Component {
  state = {
    roomData:{
      roomName: null,
      location: null,
      activity: true,
      availability: "08.00 - 16.00",
      numberOfPeople: null,
      flipchart: false,
      soundSystem: false,
    },
    error: 'default',
    succes: 'default'
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

  handleCheck = (name, value) => {
    this.setState(prevState =>({
      ...prevState,
      roomData: {
        ...prevState.roomData,
        [name]: value
      }
    }), () =>{
      console.log(this.state.roomData)
    })
  }

  handleAChange = (name, value) =>{
    this.setState(prevState => ({
      ...prevState,
      roomData: {
        ...prevState.roomData,
        [name]: value === "Tak" ? (true) : (false)
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
      })
    }else{
      this.setState({
        succes: 'done',
        error: 'default'
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

  componentDidMount(){
    const { cookies } = this.props;
    if(cookies.get('token') === undefined){
      this.props.history.push('/');
    }
  }
  


  render() {
    return (
        <div className="add_room">
          <h2>Dodaj salę:</h2>

          <form onSubmit={this.handleSubmit} className="add_room_form">
            <label htmlFor="roomName">Nazwa sali:</label>
            <input type="text" id="roomName" onChange={e => this.handleChange('roomName', e.target.value)} required></input><br/>  

            <label htmlFor="location">Lokalizacja:</label>
            <input type="text" id="location" onChange={e => this.handleChange('location', e.target.value)} required></input><br/>  

            <label htmlFor="numberOfPeople">Liczba osób</label>
            <input type="number" id="numberOfPeople" onChange={e => this.handleNChange('numberOfPeople', e.target.value)} required></input><br/>  

            <label htmlFor="activity">Aktywność:</label>
            <select id="activity" onChange={e => this.handleAChange('activity', e.target.value)}>
              <option>Tak</option>
              <option>Nie</option>
            </select>

            <label htmlFor="availability">Dostępność:</label>
            <select id="availability" onChange={e => this.handleChange('availability', e.target.value)}>
              <option>08.00 - 16.00</option>
              <option>07.00 - 18.00</option>
              <option>07.00 - 20.00</option>
            </select><br/>

            <div className="equ_lab">
              <span>Wybierz wyposażenie:</span>
            </div>

            <div className="equip">
              <label htmlFor="flipchart">Flipchart</label>
              <input type="checkbox" value="flipchart" id="flipchart" onChange={e=>{this.handleCheck('flipchart', e.target.checked)}}></input>
           
              <label htmlFor="voice">System nagłaśniający</label>
              <input type="checkbox" value="voice" id="voice" onChange={e=>{this.handleCheck('soundSystem', e.target.checked)}}></input>
            </div>

            <input type="submit" value="Zapisz"></input>

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
