import React, { Component } from 'react';
import UserViewTemp from './UserViewTemp';
import '../styles/UserView.scss';
import { withRouter } from 'react-router-dom';
import * as JWT from 'jwt-decode';

 class UserView extends Component {
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

  render() {
    return (
      <div className="box">
        <div className="addUser" onClick={this.handleClick}>
            <p>Dodaj użytkownika</p>
        </div>
        <h3>Lista użytkowników:</h3>
        <div>
          <div className="headers">
            <p>Imię:</p>
            <p>Nazwisko:</p>
            <p>E-mail:</p>
            <p>Rola:</p>
            <p>Status:</p>
            <p className={this.state.visible}>Opcje:</p>
          </div>
          <UserViewTemp ip={this.props.ip} cookies={this.props.cookies}/>
        </div>
      </div>
    )
  }
}

export default withRouter(UserView);