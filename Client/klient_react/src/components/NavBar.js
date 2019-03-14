import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import logo from '../img/mobitouch.png';
import * as JWT from 'jwt-decode';

export default class NavBar extends Component {

  state = {
    visible: 'none'
  }

  componentDidMount(){
    const { cookies } = this.props;
    if(cookies.get('token') !== undefined){
    var token = cookies.get('token');
    let t = JWT(token);
    if(t.role === "Administrator"){
      this.setState({
        visible: null
      })
    }
  }}



  render() {
    return (
      <nav>
        <img src={logo} alt="logo"></img>
        <h1>MobiReservation <br/> System</h1>
        <ul>
          <Link to="/home"><li>Dashboard</li></Link>
          <li>Rezerwacja Sali
            <ul>
              <Link to="/roomReserv"><li>Zarezerwuj sale</li></Link>
              <li>Lista sal/rezerwacje</li>
              <Link to="/addRoom"><li className={this.state.visible}>Dodaj sale</li></Link>
            </ul>
          </li>

          <Link to="addUser"><li>Użytkownicy</li></Link>
          <li>Ustawienia</li>
        </ul>
      </nav>
    )
  }
}
