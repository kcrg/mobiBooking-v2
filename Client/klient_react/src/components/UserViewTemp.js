import React, { Component } from 'react';
import { library } from '@fortawesome/fontawesome-svg-core';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faUserEdit, faUserTimes, faUserCheck} from '@fortawesome/free-solid-svg-icons';
import * as JWT from 'jwt-decode';
import axios from 'axios';

library.add(faUserEdit, faUserTimes, faUserCheck)

export default class userViewTemp extends Component {

    state = {
        usersData: [],
        mappedUsers: null,
        visible: null,
    }

    componentDidMount(){
        const { ip } = this.props
        axios.get( ip + '/api/Users/get_all')
        .then(res =>{
            console.log(res.data)
            this.setState({
                usersData: res.data
            }, this.mapUserData)
        })
        .catch(err =>{
            console.log(err)
        })

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
            axios.get( ip + '/api/Users/get_all')
            .then(res =>{
                this.setState({
                    usersData: res.data
                }, this.mapUserData)
            })
            .catch(err =>{
                console.log(err)
            })
        })
        .catch(err =>{
            console.log(err)
        })
     }

     handleEditClick = () =>{
        
     }

    mapUserData = () =>{
        const user = this.state.usersData.map(user =>{
            let ikona = faUserTimes
            if(user.active === false){
                ikona = faUserCheck
            }
            return(
                <div className="user_box" key={user.id}>
                    <p>{user.name}</p>
                    <p>{user.surname}</p>
                    <p>{user.email}</p>
                    <p>{user.role}</p>
                    <p>{user.active ? ("Aktywny") : ("Nie aktywny")}</p>
                    <p className={this.state.visible}>
                        <FontAwesomeIcon icon={faUserEdit} title="Edytuj użytkownika" value={user.id} onClick={e =>{this.handleEditClick(user.id)}}></FontAwesomeIcon>
                        <FontAwesomeIcon icon={ikona} title="Zmień status użytkownika"  onClick={e => {this.handleDeactivateClick(user.id, user.active)}}></FontAwesomeIcon>
                    </p>
                </div>
            )
        })
        this.setState({
            mappedUsers: user
        })
    }

  render() {
    return (
        <div className="test">
           {this.state.mappedUsers}
        </div>
    )
  }
}
