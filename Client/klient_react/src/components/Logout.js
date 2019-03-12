import React, { Component } from 'react';
import '../css/Logout.scss';
import { withRouter } from 'react-router-dom';
import axios from 'axios';



class Logout extends Component {

  handleClick = () =>{
    const { cookies } = this.props;
    const { ip } = this.props;
    axios.post(ip + '/api/Authenticate/Logout',"")
    .then(res =>{
      cookies.remove('token');
      this.props.history.push('/');
    }).catch(err =>{
      cookies.remove('token');
      console.log(cookies.get('token'))
      this.props.history.push('/');
    })
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
