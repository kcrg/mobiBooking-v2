import React, { Component } from 'react';
import UserViewTemp from './UserViewTemp';
import '../styles/UserView.scss';
import { withRouter } from 'react-router-dom';

 class UserView extends Component {

    handleClick = () =>{
        this.props.history.push('/addUser')
    }

  render() {
    return (
      <div className="box">
        <div className="addUser" onClick={this.handleClick}>
            <p>Dodaj użytkownika</p>
        </div>

        <h3>Lista użytkowników:</h3>
        <UserViewTemp ip={this.props.ip}/>
      </div>
    )
  }
}

export default withRouter(UserView);