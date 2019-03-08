import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import logo from '../img/mobitouch.png';
import '../css/NavBar.scss'

export default class NavBar extends Component {

  componentDidMount(){
    document.body.style.backgroundColor = "#fff";
  }

  render() {
    return (
      <nav>
          <img src={logo} alt="logo"></img>
          <h1>MobiReservation <br/> System</h1>
          <ul>
              <Link to="home" style={{textDecoration: 'none', color:'#222'}}><li className="outside">Dashboard</li></Link>
              <li className="outside">Rezerwacja Sali
                  <ul>
                    <li className="inside">Zarezerwuj sale</li>
                    <li className="inside">Lista sal/rezerwacje</li>
                    <li className="inside">Dodaj sale</li>
                  </ul>
              </li>

              <Link to="addUser" style={{textDecoration: 'none', color:'#222'}}><li className="outside">Użytkownicy</li></Link>
              <li className="outside">Ustawienia</li>
          </ul>
      </nav>
    )
  }
}
