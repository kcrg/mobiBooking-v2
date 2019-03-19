import React, { Component } from 'react';
import { withRouter } from 'react-router-dom';
import axios from 'axios';
import '../styles/Logout.scss';
import logo from '../img/mobitouch.png';

class Logout extends Component {

  componentDidMount(){
    const { cookies } = this.props
    axios.interceptors.request.use(function(config) {
      const token = cookies.get('token');
      if( token != null ){
        config.headers.Authorization = `Bearer ${token}`;
      }
      return config;
    },function(err){
        return Promise.reject(err);
      });
  }

  handleClick = () =>{
    const { cookies } = this.props;
    cookies.remove('token');
    this.props.history.push('/');
  }
 
  render() {
    return (
      <div className="logout">
      <div className="nav-logo">
            <img src={logo} alt="logo"></img>
            <h1>MobiReservation <br/> System</h1>
          </div>
        <button onClick={this.handleClick} className="btn_logout">Wyloguj</button>
      </div>
    )
  }
}

export default withRouter(Logout)
