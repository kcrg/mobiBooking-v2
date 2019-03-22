import React, { Component } from 'react';
import axios from 'axios';
import '../styles/UsersView.scss';
import { withRouter } from 'react-router-dom';
import { library } from '@fortawesome/fontawesome-svg-core';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faUserEdit, faUserClock} from '@fortawesome/free-solid-svg-icons';

library.add(faUserEdit, faUserClock)



 class UsersView extends Component {

    state = {
        usersData: [],
        name: [],
        surname: [],
        email: [],
        role: [],
        active: [],
        options: [],
        id: null
    }

    componentDidMount(){
        const { ip, cookies } = this.props
        if(cookies.get('token') === undefined){
            this.props.history.push('/');
          }
        axios.get( ip + '/api/Users/get_all')
        .then(res =>{
            this.setState({
                usersData: res.data,
            }, this.launchAllMaps)
        })
        .catch(err =>{
            console.log(err)
        })
    }

    mapName = () =>{
        const name = this.state.usersData.map(user =>{
            return(
                <p key={user.id}>{user.name}</p>
            )
        })
        this.setState({
            name
        })
    }

    mapSurname = () =>{
        const surname = this.state.usersData.map(user =>{
            return(
                <p key={user.id}>{user.surname}</p>
            )
        })
        this.setState({
            surname
        })
    }

    mapEmail = () =>{
        const email = this.state.usersData.map(user =>{
            return(
                <p key={user.id}>{user.email}</p>
            )
        })
        this.setState({
            email
        })
    }

    mapRole = () =>{
        const role = this.state.usersData.map(user =>{
            return(
                <p key={user.id}>{user.role}</p>
            )
        })
        this.setState({
            role
        })
    }

    mapActive = () =>{
        const active = this.state.usersData.map(user =>{
            user.active === true ? (user.active = "Tak"): (user.active = "Nie")
            return(
                <p key={user.id}>{user.active}</p>
            )
        })
        this.setState({
            active
        })
    }

    mapOptions = () =>{
        const options = this.state.usersData.map(user =>{
            return(
                <p key={user.id} className="icons">
                    <FontAwesomeIcon icon={faUserEdit} title="Edytuj użytkownika" onClick={() => {this.handleEditClick(user.id)}}></FontAwesomeIcon>
                    <FontAwesomeIcon icon={faUserClock} title="Dezaktywuj użytkownika" onClick={() => {this.handleDeactivateClick(user.id)}}></FontAwesomeIcon>
                </p>
            )
        })
        this.setState({
            options
        })
    }

    handleEditClick = (id) =>{
        this.setState({
            id
        })
    }
   
    launchAllMaps = () =>{
        this.mapName();
        this.mapSurname();
        this.mapEmail();
        this.mapRole();
        this.mapActive();
        this.mapOptions()
    }


  render() {
    return (
        <div className="box">
            <div className="users_grid">
            
                <div className="name">
                    <h3>Imię:</h3>
                    {this.state.name}
                </div>

                <div className="surname">
                <h3>Nazwisko:</h3>
                    {this.state.surname}
                </div>

                <div className="email">
                <h3>E-mail:</h3>
                    {this.state.email}
                </div>

                <div className="role">
                <h3>Rola:</h3>
                    {this.state.role}
                </div>

                <div className="active">
                <h3>Aktywny:</h3>
                    {this.state.active}
                </div>

                <div className="options">
                    <h3>Opcje:</h3>
                    {this.state.options}
                </div>
        </div>
      </div>
    )
  }
}

export default withRouter(UsersView)
