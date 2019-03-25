import React, { Component } from 'react';
import { library } from '@fortawesome/fontawesome-svg-core';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faUserEdit, faUserTimes} from '@fortawesome/free-solid-svg-icons';
import axios from 'axios'

library.add(faUserEdit, faUserTimes)

export default class userViewTemp extends Component {

    state = {
        usersData: [],
        mappedUsers: null
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
    }

    mapUserData = () =>{
        const user = this.state.usersData.map(user =>{
            
            return(
                <div className="user_box" key={user.id}>
                    <p>{user.name}</p>
                    <p>{user.surname}</p>
                    <p>{user.email}</p>
                    <p>{user.role}</p>
                    <p>{user.active ? ("Aktywny") : ("Nie aktywny")}</p>
                    <p>
                        <FontAwesomeIcon icon={faUserEdit} title="Edytuj użytkownika"></FontAwesomeIcon>
                        <FontAwesomeIcon icon={faUserTimes} title="Zmień aktywność użytkownika"></FontAwesomeIcon>
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
            <div className="headers">
                <p>Imię:</p>
                <p>Nazwisko:</p>
                <p>E-mail:</p>
                <p>Rola:</p>
                <p>Aktywnośc:</p>
                <p>Opcje:</p>
            </div>
            {this.state.mappedUsers}
        </div>
    )
  }
}
