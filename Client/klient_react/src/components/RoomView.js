import React, { Component } from 'react';
import '../styles/RoomView.scss';
import { withRouter } from 'react-router-dom';
import * as JWT from 'jwt-decode';
import RoomViewTemp from './RoomViewTemp';
import { library } from '@fortawesome/fontawesome-svg-core';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faSortAmountDown, faSortAmountUp } from '@fortawesome/free-solid-svg-icons';
import getRooms from '../actions/RoomViewAction';
import getRoomsNumber from '../actions/GetRoomsNumber'
import { connect } from 'react-redux';

library.add(faSortAmountDown, faSortAmountUp);

 class RoomView extends Component {
    state = {
      visible: null
    }

    handleClick = () =>{
        this.props.history.push('/addUser')
    }

    componentDidMount(){
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

    handleSortCount = () =>{
      this.props.getRooms(this.props.ip)
    }

    handleSortName = () =>{
      this.props.getRoomsNumber(this.props.ip)
    }

  render() {
    return (
      <div className="box">
        <h3>Lista sal:</h3>
        <div className="sort">
          <span>Posortuj po nazwie:</span>
          <FontAwesomeIcon onClick={this.handleSortCount} icon={faSortAmountDown}></FontAwesomeIcon>
          <span>Posortuj po liczbie:</span>
          <FontAwesomeIcon onClick={this.handleSortName} icon={faSortAmountUp}></FontAwesomeIcon>
        </div>
        <div>
          <div className="room_headers">
            <p>ID:</p>
            <p>Nazwa sali:</p>
            <p>Lokalizacja:</p>
            <p>Dostępność:</p>
            <p>Maks. liczba osób:</p>
            <p>Opcje:</p>
          </div>
          <RoomViewTemp ip={this.props.ip} cookies={this.props.cookies}/>
        </div>
      </div>
    )
  }
}

const mapDispatchToProps = (dispatch) =>{
  return {
      getRooms: (ip) => {dispatch(getRooms(ip))},
      getRoomsNumber: (ip) => {dispatch(getRoomsNumber(ip))},
  }
}

export default connect(null, mapDispatchToProps)(withRouter(RoomView));