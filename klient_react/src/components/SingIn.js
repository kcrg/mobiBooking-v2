import React, { Component } from 'react';
import '../css/SignIn.scss';
import logo from '../img/mobitouch.png';
import { Link } from 'react-router-dom';

export default class SingIn extends Component {

    state = {
        Email: '',
        Password: ''
    }

    componentDidMount(){
      document.body.style.backgroundColor = "#8d1be5";
    }

    handleChange = (e) =>{
        this.setState({
            [e.target.id]: e.target.value
        })   
    }

    handleSubmit = (e) =>{
        e.preventDefault();
        this.props.addPass(this.state);
    }
  render() {
    return (
        <div id="log">
          <img src={logo} alt="logo" id="logging"></img>

          <form onSubmit={this.handleSubmit}>
              <label htmlFor="email" id="mail">E-mail:</label>
              <input type="email" id="email" onChange={this.handleChange}></input><br/>
              <label htmlFor="password" id="pass">Hasło:</label>
              <input type="password" id="password" onChange={this.handleChange}></input><br/>

              <Link to="/home"><input type="submit" value="Zaloguj się"></input></Link>
          </form>
        </div>
    )
  }
}
