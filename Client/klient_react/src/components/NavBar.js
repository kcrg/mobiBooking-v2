import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import logo from '../img/mobitouch.png';
import '../css/NavBar.scss';
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
    document.body.style.backgroundColor = "#fff";
  }}



  render() {
    return (
      <nav>
          <img src={logo} alt="logo"></img>
          <h1>MobiReservation <br/> System</h1>
          <ul>
              <Link to="/home" style={{textDecoration: 'none', color:'#222'}}><li className="outside">Dashboard</li></Link>
              <li className="outside">Rezerwacja Sali
                  <ul>
                    <Link to="/roomReserv" style={{textDecoration: 'none', color:'#222'}}><li className="inside">Zarezerwuj sale</li></Link>
                    <li className="inside">Lista sal/rezerwacje</li>
                   <Link to="/addRoom" style={{textDecoration: 'none', color:'#222'}}><li className={this.state.visible}>Dodaj sale</li></Link>
                  </ul>
              </li>

              <Link to="addUser" style={{textDecoration: 'none', color:'#222'}}><li className="outside">Użytkownicy</li></Link>
              <li className="outside">Ustawienia</li>
          </ul>
      </nav>
    )
  }
}
