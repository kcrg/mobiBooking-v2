import React, { Component } from 'react';
import logo from '../img/mobitouch.png';
import '../css/NavBar.scss'

export default class NavBar extends Component {
  render() {
    return (
      <nav>
          <img src={logo} alt="logo"></img>
          <h1>MobiReservation <br/> System</h1>

          <ul>
              <li className="outside">Dashboard</li>
              <li className="outside">Rezerwacja Sali
                  <ul>
                    <li className="inside">Zarezerwuj sale</li>
                    <li className="inside">Lista sal/rezerwacje</li>
                    <li className="inside">Dodaj sale</li>
                  </ul>
              </li>

              <li className="outside">Użytkownicy</li>
              <li className="outside">Ustawienia</li>
          </ul>
      </nav>
    )
  }
}
