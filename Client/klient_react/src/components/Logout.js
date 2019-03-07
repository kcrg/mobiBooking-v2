import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import '../css/Logout.scss';

export default class Logout extends Component {
  render() {
    return (
      <div id="options">
        <Link to="/"><button>Wyloguj</button></Link>
      </div>
    )
  }
}
