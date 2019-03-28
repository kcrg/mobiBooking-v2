import React, { Component } from 'react';
import { library } from '@fortawesome/fontawesome-svg-core';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faUserEdit, faUserTimes, faUserCheck} from '@fortawesome/free-solid-svg-icons';
import * as JWT from 'jwt-decode';
import axios from 'axios';
import { connect } from 'react-redux';
import getUsers from '../actions/UserViewAction';
import editUser from '../actions/EditUserAction';
import { withRouter } from 'react-router-dom';

library.add(faUserEdit, faUserTimes, faUserCheck)

class userViewTemp extends Component {

    state = {
        visible: null
    }

    componentDidMount(){
        this.props.getUsers(this.props.ip)
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

    handleDeactivateClick = (id, active) =>{
        const { ip } = this.props
        axios.put( ip + '/api/Account/update_activity/' + id + '/' + !active)
        .then(res =>{
          this.props.getUsers(ip)
        })
        .catch(err =>{
            console.log(err)
        })
     }

     handleEditClick = (id) =>{
        this.props.editUser(id, this.props.ip);
        this.props.history.push('/editUser');
     }

     countUsers = () =>{
        const liczba =  this.props.usersData.length;
        console.log(liczba)
     }

  render() {
    let count = this.props.usersData.length
    let mapUserData = (
        this.props.usersData.map(user =>{
            let ikona = faUserTimes
            if(user.active === false){
                ikona = faUserCheck
            }
            return(
                <div className="user_box" key={user.id}>
                    <p>{user.name}</p>
                    <p>{user.surname}</p>
                    <p className="email">{user.email}</p>
                    <p>{user.role}</p>
                    <p>{user.active ? ("Aktywny") : ("Nie aktywny")}</p>
                    <p className={this.state.visible}>
                        <FontAwesomeIcon icon={faUserEdit} title="Edytuj użytkownika" value={user.id} onClick={e =>{this.handleEditClick(user.id)}}></FontAwesomeIcon>
                        <FontAwesomeIcon icon={ikona} title="Zmień status użytkownika"  onClick={e => {this.handleDeactivateClick(user.id, user.active)}}></FontAwesomeIcon>
                    </p>
                </div>
            )
        })
    )
    return (
        <div className="test">
          {mapUserData}
          <div className="user_count">
            <p>Liczba użytkowników: <span>{count}</span></p>
          </div>
        </div>
    )
  }
}

const mapStateToProps = (state) =>{
    return{
        usersData: state.usersData
    }
}

const mapDispatchToProps = (dispatch) =>{
    return {
        getUsers: (ip) => {dispatch(getUsers(ip))},
        editUser: (id, ip) => {dispatch(editUser(id,ip))}
    }
}
export default connect(mapStateToProps, mapDispatchToProps)(withRouter(userViewTemp));
