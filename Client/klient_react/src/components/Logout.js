import React, { Component } from 'react';
import '../css/Logout.scss';
import { withRouter } from 'react-router-dom';
import axios from 'axios';




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
      <div id="options">
        <button onClick={this.handleClick}>Wyloguj</button>
      </div>
    )
  }
}

export default withRouter(Logout)
