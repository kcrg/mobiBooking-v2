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
import reservRoom from '../actions/PassId';

library.add(faEdit, faTrashAlt)

class RoomViewTemp extends Component {

    state = {
        visible: null,
        deletePop: 'hidden',
        roomIdentify: null,
        message: 'default'
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

    handlePopClick = (id) =>{
        this.setState({
            deletePop: 'showMe',
            roomIdentify: id
        })
    }

    handleYes = () =>{
        axios.delete(this.props.ip + '/api/Room/delete/' + this.state.roomIdentify)
        .then(res =>{
            this.setState({
                deletePop: 'hidden'
            })
            this.props.getRooms(this.props.ip)
        })
        .catch(err =>{
            this.setState({
                message: 'taken',
                deletePop: 'hidden'
            })
            setTimeout(() =>{
                this.setState({message: 'default'});
               }, 3000);
        })
    }

    handleNo = () =>{
        this.setState({
            deletePop: 'hidden'
        })
    }

    handleReservClick = (id) =>{
        const data = true;
        this.props.reservRoom(id, data)
        this.props.history.push('/roomReserv')
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
                        <button className="btn" onClick={e => {this.handleReservClick(room.id)}}>Zarezerwuj salę</button>
                        <span className={this.state.visible}>
                            <FontAwesomeIcon icon={faEdit} title="Edytuj salę" onClick={e => {this.handleEditClick(room.id)}}></FontAwesomeIcon>
                            <FontAwesomeIcon icon={faTrashAlt} title="Usuń salę" onClick={e => {this.handlePopClick(room.id)}}></FontAwesomeIcon>
                        </span>
                  </p>
                </div>
            )
        })
    )
    return (
        <div className="test">
          {mapRoomData}
          <div className={this.state.deletePop}>
          <div className="info">
                <p>Czy na pewno chcesz usunąć ?</p>
                <div className="decide">
                    <span onClick={this.handleYes}>Tak</span>
                    <span onClick={this.handleNo}>Nie</span>
                </div>
            </div>
          </div>
          <div className={this.state.message}><p>Ta sala jest zarezerwowana ! Nie można jej usunąć :)</p></div>
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
        editRoom: (id, ip) => {dispatch(editRoom(id, ip))},
        reservRoom: (id, data) => {dispatch(reservRoom(id, data))}
    }
}
export default connect(mapStateToProps, mapDispatchToProps)(withRouter(RoomViewTemp));
