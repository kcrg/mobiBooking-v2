import React, { Component } from 'react';
import axios from 'axios';
import { withRouter } from 'react-router-dom';
import '../styles/AddRoom.scss';
import { connect } from 'react-redux';
import updateRoom from '../actions/UpdateRoom';
import getAva from '../actions/GetAva'

class EditRoom extends Component {
  state = {
    error: 'default',
    succes: 'default',
    warning: 'default',
    mapAva: null,
    value: null
  }

  componentDidMount(){
      this.props.getAva(this.props.ip)
  }

  componentWillReceiveProps(nextProps){
     this.setState({
         value: nextProps.room.availability
     })
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
      this.updateData()
  }

  checkData = () =>{
    return (this.props.room.roomName.match(/^ *$/) !== null ||
    this.props.room.location.match(/^ *$/) !== null||
    this.props.room.numberOfPeople === '')
  }


  updateData = () =>{
    const { ip } = this.props;
    const { id } = this.props
    parseInt(this.props.room.numberOfPeople);
    axios.put(ip + '/api/Room/update/' + id, this.props.room)
    .then(res => {
      this.toggleError(false)
      return res;
    })
    .catch(err =>{
      this.toggleError(true)
    });
  }

  handleChange = (name, value) =>{
    this.props.updateRoom(name, value);
  }

  render() {
    let mapAva = (
        this.props.ava.map(ava =>{
            return(
                <option key={ava.id} value={ava.id}>{ava.name}</option>
            )
        })
    )
    return (
        <div className="add_room">
          <h2>Edytuj salę:</h2>
          <form onSubmit={this.handleSubmit} className="add_room_form">
            <div className="room_label">
              <div className="room">
                <label htmlFor="roomName">Nazwa sali:</label>
              </div>
              <div className="room_input">
                <input type="text" id="roomName" value={this.props.room.roomName} onChange={e => this.handleChange('roomName', e.target.value)} placeholder="Nazwa sali"></input>
              </div>
            </div>

            <div className="location">
              <div className="location_label">
                <label htmlFor="location">Lokalizacja:</label>
              </div>
              <div className="location_input">
                <input type="text" id="location" value={this.props.room.location} onChange={e => this.handleChange('location', e.target.value)} placeholder="Lokalizacja"></input> 
              </div>
            </div>

            <div className="number_of_people">
              <div className="number_label">
                <label htmlFor="numberOfPeople">Liczba osób:</label>
              </div>
              <div className="number_input">
                <input type="number" id="numberOfPeople" value={this.props.room.numberOfPeople} onChange={e => this.handleChange('numberOfPeople', e.target.value)}></input>
              </div> 
            </div>

            <div className="availability">
              <div className="ava_label">
                <label htmlFor="availability">Dostępność:</label>
              </div>
              <div className="ava_select">
                <select id="availability" value = {this.props.room.availabilityId} onChange={e => this.handleChange('availabilityId', parseInt(e.target.value))}>
                  {mapAva}
                </select>
              </div>
            </div>

            <div className="add_room_submit">
              <input type="submit" value="Zapisz"></input>
            </div>

            <div className={this.state.error}>
              <p>Błąd! Spróbuj ponownie</p>
            </div>

            <div className={this.state.succes}>
              <p>Zaktualizowano dane!</p>
            </div>

            
            <div className={this.state.warning}>
              <p>Uzupełnij wszystkie pola !</p>
            </div>

          </form>
      </div>
    )
  }
}

const mapStateToProps = (state) =>{
    return{
        id: state.roomId,
        room: state.room,
        ava: state.ava
    }
  }
  
  const mapDispatchToProps = (dispatch) =>{
      return {
          getAva: (ip) => {dispatch(getAva(ip))},
          updateRoom: (name, value) => {dispatch(updateRoom(name,value))}
      }
  }
  

export default connect(mapStateToProps, mapDispatchToProps )(withRouter(EditRoom));
