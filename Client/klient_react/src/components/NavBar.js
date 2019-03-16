import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import logo from '../img/mobitouch.png';
import * as JWT from 'jwt-decode';
import '../styles/NavBar.scss';
import { library } from '@fortawesome/fontawesome-svg-core'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faArrowAltCircleRight} from '@fortawesome/free-solid-svg-icons'

library.add(faArrowAltCircleRight)

export default class NavBar extends Component {

  state = {
    visible: 'none',
    showNav: 'navigation',
    btn_show: 'show'
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

  showNav = () =>{
    if(this.state.showNav === 'navigation'){
      this.setState({
        showNav: 'navigation-showed',
        btn_show: 'hide'
      },() =>{
        console.log(this.state.showNav)
      })
    }else{
      this.setState({
        showNav: 'navigation',
        btn_show: 'show'
      })
    }
  }



  render() {
    return (
      <div className="nav-container">
        <nav className={this.state.showNav}> 
          <ul className="navigation-links">
            <Link to="/home"><li>Dashboard</li></Link>
            <Link to="addUser"><li>Rezerwacja Sali</li></Link>

            <ul className="reserv-links">
                <Link to="/roomReserv"><li>Zarezerwuj sale</li></Link>
                <Link to="/555"><li>Lista sal/rezerwacje</li></Link>
            </ul>

            <Link to="/addRoom"><li className={this.state.visible}>Dodaj sale</li></Link>
            <Link to="addUser"><li>Użytkownicy</li></Link>
            <Link to="addUser"><li>Ustawienia</li></Link>
          </ul>
        </nav>

        <FontAwesomeIcon icon={faArrowAltCircleRight} onClick={this.showNav} className={this.state.btn_show}>Hello</FontAwesomeIcon>
      </div>
    )
  }
}
