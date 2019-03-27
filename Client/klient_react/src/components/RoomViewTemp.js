import React, { Component } from 'react';
import { library } from '@fortawesome/fontawesome-svg-core';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faEdit, faTrashAlt} from '@fortawesome/free-solid-svg-icons';
import * as JWT from 'jwt-decode';
import axios from 'axios';
import { connect } from 'react-redux';
import getRooms from '../actions/RoomViewAction';
import { withRouter } from 'react-router-dom';
import editRoom from '../actions/EditRoom';

library.add(faEdit, faTrashAlt)

class RoomViewTemp extends Component {

    state = {
        visible: null
    }

    componentDidMount(){
        this.props.getRooms(this.props.ip)
        const { cookies } = this.props;
        if(cookies.get('token') !== undefined){
            var token = cookies.get('token');
            let t = JWT(token);
            if(t.role === "User"){
                this.setState({
                    visible: 'hidden'
                })
            }
        }
    }

    handleEditClick = (id) =>{
        this.props.editRoom(id, this.props.ip);
        this.props.history.push('/editRoom')
    }

    handleDeleteClick = (id) =>{
        axios.delete( this.props.ip + '/api/Room/delete/' + id )
        .then(res =>{
            this.props.getRooms(this.props.ip)
        })
        .catch(err =>{
        })
        
    }

  render() {
    let mapRoomData = (
        this.props.roomData.map(room =>{
            return(
                <div className="room_box" key={room.id}>
                  <p>{room.id}</p>
                  <p>{room.roomName}</p>
                  <p>{room.location}</p>
                  <p>{room.availability}</p>
                  <p>{room.numberOfPeople}</p>
                  <p>
                        <button className="btn">Zarezerwuj salę</button>
                        <FontAwesomeIcon icon={faEdit} title="Edytuj salę" onClick={e => {this.handleEditClick(room.id)}}></FontAwesomeIcon>
                        <FontAwesomeIcon icon={faTrashAlt} title="Usuń salę" onClick={e => {this.handleDeleteClick(room.id)}}></FontAwesomeIcon>
                  </p>
                </div>
            )
        })
    )
    return (
        <div className="test">
          {mapRoomData}
        </div>
    )
  }
}

const mapStateToProps = (state) =>{
    return{
        roomData: state.roomData,
        room: state.room
    }
}

const mapDispatchToProps = (dispatch) =>{
    return {
        getRooms: (ip) => {dispatch(getRooms(ip))},
        editRoom: (id, ip) => {dispatch(editRoom(id, ip))}
    }
}
export default connect(mapStateToProps, mapDispatchToProps)(withRouter(RoomViewTemp));
