import React, { Component } from 'react';
import '../css/Logout.scss';
import { withRouter } from 'react-router-dom';




class Logout extends Component {

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
